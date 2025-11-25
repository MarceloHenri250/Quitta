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
        private DataService dataService;
        private List<Item> items;
        private List<MonthlyBudget> budgets;
        private NotificationSettings settings;

        public MainForm()
        {
            InitializeComponent();
            dataService = new DataService();
            dashboardControl = new UserControls.DashboardControl();
            dashboardControl.Dock = DockStyle.Fill;
            tabDashboard.Controls.Clear();
            tabDashboard.Controls.Add(dashboardControl);

            // wire budget control event
            budgetAnualControl.BudgetsChanged += () =>
            {
                // get current budgets from the control (it was the source of the change)
                budgets = budgetAnualControl.GetBudgets();
                SaveData();
                // refresh other views
                dashboardControl.SetData(items, budgets);
                // ensure control and others are in sync
                budgetAnualControl.SetData(items, budgets);
            };

            LoadData();

            // wire cadastro usercontrol events (ItemCreated)
            cadastrarControl.ItemCreated += CadastrarControl_ItemCreated;

            // wire listagem and relatorio usercontrols
            listagemControl.PageChanged += page => { /* could update page label */ };
            // handle item updates/deletes by syncing main list and persisting
            listagemControl.ItemUpdated += item =>
            {
                var idx = items.FindIndex(i => i.Id == item.Id);
                if (idx >= 0) items[idx] = item;
                SaveData();
                // refresh views
                dashboardControl.SetData(items, budgets);
                relatorioControl.SetData(items);
                listagemControl.SetData(items);
                // keep budget anual in sync (item status affects paid/pending calculations)
                budgetAnualControl.SetData(items, budgets);
            };

            listagemControl.ItemDeleted += item =>
            {
                // remove from main list if present
                items.RemoveAll(i => i.Id == item.Id);
                SaveData();
                dashboardControl.SetData(items, budgets);
                relatorioControl.SetData(items);
                listagemControl.SetData(items);
                budgetAnualControl.SetData(items, budgets);
            };

            relatorioControl.ExportPdfRequested += () => { };
            // Export handled inside RelatorioControl; keep event for compatibility but no placeholder message
            relatorioControl.ExportExcelRequested += () => { }; 
            // Note: RelatorioControl filters should not affect the Listagem tab.
            // Do not forward RelatorioControl.FilterApplied to listagemControl to keep views independent

            // populate controls with data
            listagemControl.SetData(items);
            relatorioControl.SetData(items);
        }

        // Make LoadData public so other controls (ConfiguracaoControl) can trigger reload when settings change
        public void LoadData()
        {
            items = dataService.LoadItems();
            budgets = dataService.LoadBudgets();
            settings = dataService.LoadSettings();

            // marcar como vencido automaticamente itens com vencimento passado
            var today = DateTime.Now.Date;
            bool changed = false;
            foreach (var it in items)
            {
                // only mark pending items as vencido; do not overwrite manually set 'Pago' or other statuses
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

            //Aqui voce vai chamar os metodos para popular cada aba
            dashboardControl.SetData(items, budgets);
            budgetAnualControl.SetData(items, budgets);
            //etc...
        }

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

        public void SaveData()
        {
            dataService.SaveItems(items);
            dataService.SaveBudgets(budgets);
            dataService.SaveSettings(settings);
        }

        private void AtualizarCorSaldo(decimal saldo)
        {
            lblValorSaldo.Text = saldo.ToString("C");
            lblValorSaldo.ForeColor = saldo >= 0 ? Color.Green : Color.Red;
        }

        // New handler for item created inside CadastrarControl
        private void CadastrarControl_ItemCreated(Item novoItem)
        {
            // Verificar número duplicado
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

            // refresh views
            dashboardControl.SetData(items, budgets);
            listagemControl.SetData(items);
            relatorioControl.SetData(items);
            budgetAnualControl.SetData(items, budgets);
        }

        private void LimparFormulario()
        {
            // delegate to control
            cadastrarControl.ClearForm();
        }

    }
}
