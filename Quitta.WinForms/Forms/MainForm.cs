using Quitta.Models;
using Quitta.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitta.Forms
{
    public partial class MainForm : Form
    {
        #region Campos privados
        private DataService dataService;
        private List<Item> items;
        private List<MonthlyBudget> budgets;
        private NotificationSettings settings;
        #endregion

        #region Construtor / Inicialização
        public MainForm()
        {
            InitializeComponent();
            dataService = new DataService();

            // Inicializa e adiciona DashboardControl dinamicamente
            dashboardControl = new UserControls.DashboardControl();
            dashboardControl.Dock = DockStyle.Fill;
            tabDashboard.Controls.Clear();
            tabDashboard.Controls.Add(dashboardControl);

            // Assina evento de alteração de budgets do controle anual
            budgetAnualControl.BudgetsChanged += () =>
            {
                // obter budgets atuais do controle (fonte da alteração)
                budgets = budgetAnualControl.GetBudgets();
                SaveData();
                // atualizar outras views
                dashboardControl.SetData(items, budgets);
                // garantir sincronização
                budgetAnualControl.SetData(items, budgets);
            };

            // carregar dados iniciais
            LoadData();

            // Eventos de outros controles
            cadastrarControl.ItemCreated += CadastrarControl_ItemCreated;

            listagemControl.PageChanged += page => { /* poderia atualizar label de página */ };

            // Sincroniza atualizações/exclusões vindas do ListagemControl
            listagemControl.ItemUpdated += item =>
            {
                var idx = items.FindIndex(i => i.Id == item.Id);
                if (idx >= 0) items[idx] = item;
                SaveData();
                // atualizar views
                dashboardControl.SetData(items, budgets);
                relatorioControl.SetData(items);
                listagemControl.SetData(items);
                // manter budget anual em sincronia
                budgetAnualControl.SetData(items, budgets);
            };

            listagemControl.ItemDeleted += item =>
            {
                // remover da lista principal se presente
                items.RemoveAll(i => i.Id == item.Id);
                SaveData();
                dashboardControl.SetData(items, budgets);
                relatorioControl.SetData(items);
                listagemControl.SetData(items);
                budgetAnualControl.SetData(items, budgets);
            };

            // Eventos de export do relatório (mantidos por compatibilidade)
            relatorioControl.ExportPdfRequested += () => { };
            relatorioControl.ExportExcelRequested += () => { };

            // popula controles com dados carregados
            listagemControl.SetData(items);
            relatorioControl.SetData(items);
        }
        #endregion

        #region Carregamento e persistência de dados
        // Tornar LoadData público para que outros controles possam solicitar recarga
        public void LoadData()
        {
            items = dataService.LoadItems();
            budgets = dataService.LoadBudgets();
            settings = dataService.LoadSettings();

            // marcar automaticamente como vencido itens pendentes com vencimento passado
            var today = DateTime.Now.Date;
            bool changed = false;
            foreach (var it in items)
            {
                // apenas marcar Pendente -> Vencido, não sobrescrever Pago
                if (it.Status == StatusItem.Pendente && it.Vencimento.Date <= today)
                {
                    it.Status = StatusItem.Vencido;
                    changed = true;
                }
            }
            if (changed)
            {
                dataService.SaveItems(items);
            }

            // popular abas/controles com os dados carregados
            dashboardControl.SetData(items, budgets);
            budgetAnualControl.SetData(items, budgets);
            // outros controles podem ser atualizados conforme necessário
        }

        public void SaveData()
        {
            dataService.SaveItems(items);
            dataService.SaveBudgets(budgets);
            dataService.SaveSettings(settings);
        }
        #endregion

        #region Handlers de UI (botões e ações simples)
        private void btnSair_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Tem certeza que deseja sair?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void AtualizarCorSaldo(decimal saldo)
        {
            lblValorSaldo.Text = saldo.ToString("C");
            lblValorSaldo.ForeColor = saldo >= 0 ? Color.Green : Color.Red;
        }
        #endregion

        #region Handlers de criação/limpeza de itens (Cadastro)
        // Novo item criado a partir do CadastrarControl
        private void CadastrarControl_ItemCreated(Item novoItem)
        {
            // verificar número duplicado
            if (items.Any(i => i.Numero == novoItem.Numero))
            {
                MessageBox.Show("Já existe um item com este número.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            items.Add(novoItem);
            SaveData();

            MessageBox.Show("Item cadastrado com sucesso!", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            cadastrarControl.ClearForm();

            // atualizar views após cadastro
            dashboardControl.SetData(items, budgets);
            listagemControl.SetData(items);
            relatorioControl.SetData(items);
            budgetAnualControl.SetData(items, budgets);
        }

        private void LimparFormulario()
        {
            // delega ao controle responsável pelo formulário de cadastro
            cadastrarControl.ClearForm();
        }
        #endregion
    }
}
