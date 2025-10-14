using Newtonsoft.Json;
using Quitta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitta.Services
{
    public class DataService
    {
        private static string appRootFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SistemaFinanceiro"
        );

        // New data folder under app root
        private static string dataFolder = Path.Combine(appRootFolder, "data");

        private static string itemsFile = Path.Combine(dataFolder, "items.json");
        private static string budgetsFile = Path.Combine(dataFolder, "budgets.json");
        private static string settingsFile = Path.Combine(dataFolder, "settings.json");
        private static string attachmentsFolder = Path.Combine(dataFolder, "attachments");

        public DataService()
        {
            // ensure new folders exist
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);
            if (!Directory.Exists(attachmentsFolder))
                Directory.CreateDirectory(attachmentsFolder);

            // Migrate existing files from old location (appRootFolder) if present and not yet migrated
            try
            {
                // old paths (legacy) were directly under appRootFolder
                var oldItems = Path.Combine(appRootFolder, "items.json");
                var oldBudgets = Path.Combine(appRootFolder, "budgets.json");
                var oldSettings = Path.Combine(appRootFolder, "settings.json");
                var oldAttachments = Path.Combine(appRootFolder, "attachments");

                if (Directory.Exists(appRootFolder) && (Directory.Exists(oldAttachments) || File.Exists(oldItems) || File.Exists(oldBudgets) || File.Exists(oldSettings)))
                {
                    // move files if new ones don't already exist
                    if (File.Exists(oldItems) && !File.Exists(itemsFile))
                        File.Move(oldItems, itemsFile);
                    if (File.Exists(oldBudgets) && !File.Exists(budgetsFile))
                        File.Move(oldBudgets, budgetsFile);
                    if (File.Exists(oldSettings) && !File.Exists(settingsFile))
                        File.Move(oldSettings, settingsFile);

                    // move attachments files into new attachments folder
                    if (Directory.Exists(oldAttachments))
                    {
                        foreach (var f in Directory.GetFiles(oldAttachments))
                        {
                            var dest = Path.Combine(attachmentsFolder, Path.GetFileName(f));
                            // ensure unique
                            if (File.Exists(dest))
                            {
                                dest = GetUniqueAttachmentPath(dest);
                            }
                            File.Move(f, dest);
                        }

                        // optionally remove old attachments directory if empty
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
                // ignore migration errors
            }
        }

        // Retorna a pasta onde anexos são armazenados
        public string GetAttachmentsFolder() => attachmentsFolder;

        // Retorna um path completo para um nome de arquivo dentro da pasta de anexos
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

        //Items
        public List<Item> LoadItems()
        {
            if (!File.Exists(itemsFile))
                return GetDefaultItems();

            try
            {
                var json = File.ReadAllText(itemsFile);
                return JsonConvert.DeserializeObject<List<Item>>(json) ?? GetDefaultItems();
            }
            catch
            {
                return GetDefaultItems();
            }
        }

        public void SaveItems(List<Item> items)
        {
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(itemsFile, json);
        }

        //Budgets
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

        //Settings
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

        //Default Data
        private List<Item> GetDefaultItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Id = "1",
                    Tipo = TipoItem.Boleto,
                    Numero = "12345678",
                    Fornecedor = "Energia Elétrica SA",
                    Valor = 450.00m,
                    Vencimento = DateTime.Now.AddDays(5),
                    Status = StatusItem.Pendente
                },
                new Item
                {
                    Id = "2",
                    Tipo = TipoItem.Nota,
                    Numero = "NF-001234",
                    Fornecedor = "Fornecedor ABC Ltda",
                    Valor = 1250.50m,
                    Vencimento = DateTime.Now.AddDays(10),
                    Status = StatusItem.Pendente
                },
                new Item
                {
                    Id = "3",
                    Tipo = TipoItem.Boleto,
                    Numero = "87654321",
                    Fornecedor = "Água e Saneamento",
                    Valor = 180.00m,
                    Vencimento = DateTime.Now.AddDays(15),
                    Status = StatusItem.Pendente
                }
            };
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
    }
}
