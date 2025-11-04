using Newtonsoft.Json;
using Quitta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Quitta.Services
{
    // Serviço responsável por carregar/salvar dados do aplicativo (itens, orçamentos, configurações e anexos)
    public class DataService
    {
        #region Paths e pastas
        // Pasta base em %AppData%\SistemaFinanceiro
        private static string appRootFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SistemaFinanceiro"
        );

        // Pasta de dados dentro da pasta da aplicação
        private static string dataFolder = Path.Combine(appRootFolder, "data");

        // Arquivos de dados
        private static string itemsFile = Path.Combine(dataFolder, "items.json");
        private static string budgetsFile = Path.Combine(dataFolder, "budgets.json");
        private static string settingsFile = Path.Combine(dataFolder, "settings.json");
        private static string attachmentsFolder = Path.Combine(dataFolder, "attachments");
        #endregion

        #region Construtor e migração
        public DataService()
        {
            // Garante que as pastas de dados existam
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);
            if (!Directory.Exists(attachmentsFolder))
                Directory.CreateDirectory(attachmentsFolder);

            // Tenta migrar dados legados que ficavam diretamente em appRootFolder
            try
            {
                var oldItems = Path.Combine(appRootFolder, "items.json");
                var oldBudgets = Path.Combine(appRootFolder, "budgets.json");
                var oldSettings = Path.Combine(appRootFolder, "settings.json");
                var oldAttachments = Path.Combine(appRootFolder, "attachments");

                if (Directory.Exists(appRootFolder) && (Directory.Exists(oldAttachments) || File.Exists(oldItems) || File.Exists(oldBudgets) || File.Exists(oldSettings)))
                {
                    // Move os arquivos para o novo local se ainda não existirem no destino
                    if (File.Exists(oldItems) && !File.Exists(itemsFile))
                        File.Move(oldItems, itemsFile);
                    if (File.Exists(oldBudgets) && !File.Exists(budgetsFile))
                        File.Move(oldBudgets, budgetsFile);
                    if (File.Exists(oldSettings) && !File.Exists(settingsFile))
                        File.Move(oldSettings, settingsFile);

                    // Move os arquivos de anexos para a nova pasta de anexos
                    if (Directory.Exists(oldAttachments))
                    {
                        foreach (var f in Directory.GetFiles(oldAttachments))
                        {
                            var dest = Path.Combine(attachmentsFolder, Path.GetFileName(f));
                            // Garante nome único para evitar sobrescrever arquivos
                            if (File.Exists(dest))
                            {
                                dest = GetUniqueAttachmentPath(dest);
                            }
                            File.Move(f, dest);
                        }

                        // Remove a pasta antiga se estiver vazia
                        try
                        {
                            if (Directory.GetFiles(oldAttachments).Length == 0 && Directory.GetDirectories(oldAttachments).Length == 0)
                                Directory.Delete(oldAttachments);
                        }
                        catch { }
                    }
                }
            }
            catch
            {
                // Ignora falhas na migração para não impedir a inicialização do app
            }
        }
        #endregion

        #region Anexos
        // Retorna a pasta onde os anexos são armazenados
        public string GetAttachmentsFolder() => attachmentsFolder;

        // Retorna um caminho completo para um arquivo dentro da pasta de anexos
        public string GetAttachmentPath(string fileName)
        {
            return Path.Combine(attachmentsFolder, fileName);
        }

        // Garante um nome de arquivo único (adiciona sufixo _1, _2, ...) se já existir
        public string GetUniqueAttachmentPath(string desiredPath)
        {
            if (!File.Exists(desiredPath)) return desiredPath;

            var dir = Path.GetDirectoryName(desiredPath) ?? attachmentsFolder;
            var name = Path.GetFileNameWithoutExtension(desiredPath);
            var ext = Path.GetExtension(desiredPath);
            int i = 1;
            string candidate;
            do
            {
                candidate = Path.Combine(dir, $"{name}_{i}{ext}");
                i++;
            } while (File.Exists(candidate));

            return candidate;
        }
        #endregion

        // Expose o caminho da pasta de dados para backups
        public string GetDataFolder() => dataFolder;

        #region Itens (Load/Save)
        // Carrega a lista de itens do arquivo. Se não existir, cria um items.json vazio e retorna lista vazia.
        public List<Item> LoadItems()
        {
            if (!File.Exists(itemsFile))
            {
                var empty = new List<Item>();
                try { SaveItems(empty); } catch { }
                return empty;
            }

            try
            {
                var json = File.ReadAllText(itemsFile);
                var items = JsonConvert.DeserializeObject<List<Item>>(json);
                if (items == null)
                {
                    var empty = new List<Item>();
                    try { SaveItems(empty); } catch { }
                    return empty;
                }

                return items;
            }
            catch
            {
                var empty = new List<Item>();
                try { SaveItems(empty); } catch { }
                return empty;
            }
        }

        // Salva a lista de itens no arquivo (sobrescreve)
        public void SaveItems(List<Item> items)
        {
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(itemsFile, json);
        }
        #endregion

        #region Orçamentos (Load/Save)
        public List<MonthlyBudget> LoadBudgets()
        {
            if (!File.Exists(budgetsFile))
                return GetDefaultBudgets();

            try
            {
                var json = File.ReadAllText(budgetsFile);
                return JsonConvert.DeserializeObject<List<MonthlyBudget>>(json) ?? GetDefaultBudgets();
            }
            catch
            {
                return GetDefaultBudgets();
            }
        }

        public void SaveBudgets(List<MonthlyBudget> budgets)
        {
            var json = JsonConvert.SerializeObject(budgets, Formatting.Indented);
            File.WriteAllText(budgetsFile, json);
        }
        #endregion

        #region Configurações (Load/Save)
        public NotificationSettings LoadSettings()
        {
            if (!File.Exists(settingsFile))
                return GetDefaultSettings();

            try
            {
                var json = File.ReadAllText(settingsFile);
                return JsonConvert.DeserializeObject<NotificationSettings>(json) ?? GetDefaultSettings();
            }
            catch
            {
                return GetDefaultSettings();
            }
        }

        public void SaveSettings(NotificationSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(settingsFile, json);
        }
        #endregion

        #region Dados padrão
        // Retorna uma lista vazia — itens padrão removidos conforme pedido do usuário
        private List<Item> GetDefaultItems()
        {
            // Sem itens padrões: o usuário irá cadastrar manualmente
            return new List<Item>();
        }

        private List<MonthlyBudget> GetDefaultBudgets()
        {
            return new List<MonthlyBudget>
            {
                new MonthlyBudget { Month = DateTime.Now.ToString("yyyy-MM"), Budget = 10000 },
                new MonthlyBudget { Month = DateTime.Now.AddMonths(1).ToString("yyyy-MM"), Budget = 10000 },
                new MonthlyBudget { Month = DateTime.Now.AddMonths(2).ToString("yyyy-MM"), Budget = 12000 }
            };
        }

        private NotificationSettings GetDefaultSettings()
        {
            return new NotificationSettings
            {
                Enabled = true,
                DaysBeforeDue = 7,
                EmailNotifications = false,
                Email = "",
                DesktopNotifications = true
            };
        }
        #endregion
    }
}
