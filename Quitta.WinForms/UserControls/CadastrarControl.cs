using Quitta.Models;
using Quitta.Services;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Quitta.UserControls
{
    /// <summary>
    /// Controle de cadastro de Boleto / Nota Fiscal.
    /// Contém validações, anexos e eventos para integrar com o formulário pai.
    /// </summary>
    public partial class CadastrarControl : UserControl
    {
        #region Campos privados

        // Eventos públicos para notificar o formulário pai
        public event Action<Item>? ItemCreated;
        public event Action<Attachment>? AttachmentAdded;

        // Serviço de acesso a disco / dados
        private readonly DataService dataService = new DataService();

        // Caminho do arquivo selecionado, antes de salvar (ainda não copiado)
        private string? pendingAttachmentSourcePath;

        // Caminho absoluto do anexo depois de salvo/copied
        private string? savedAttachmentAbsolutePath;

        // P/Invoke para definir placeholder (cue banner) em TextBox nativo do Windows
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        #endregion

        #region Construtor e inicialização

        public CadastrarControl()
        {
            InitializeComponent();

            // Wiring de eventos dos botões e controles internos
            btnCadastrar.Click += BtnCadastrar_Click;
            btnLimpar.Click += (s, e) => ClearForm();

            btnAnexar.Click += BtnAnexar_Click; // botão auxiliar (designer)
            lstAnexos.DoubleClick += LstAnexos_DoubleClick;

            btnOpenAttachment.Click += BtnOpenAttachment_Click;
            btnDeleteAttachment.Click += BtnDeleteAttachment_Click;

            // Garante que o ComboBox de status esteja populado
            if (cmbStatus.Items.Count == 0)
            {
                cmbStatus.Items.Add("Pendente");
                cmbStatus.Items.Add("Pago");
                cmbStatus.Items.Add("Vencido");
            }

            if (cmbStatus.Items.Count > 0 && cmbStatus.SelectedIndex < 0)
                cmbStatus.SelectedIndex = 0;

            // Atualiza o placeholder quando o tipo (boleto/nota) muda
            rbBoleto.CheckedChanged += (s, e) => UpdateNumeroPlaceholder();
            rbNota.CheckedChanged += (s, e) => UpdateNumeroPlaceholder();

            // Atualiza os botões de anexo quando a seleção da lista muda
            lstAnexos.SelectedIndexChanged += (s, e) => UpdateAttachmentButtons();

            // placeholder inicial
            UpdateNumeroPlaceholder();

            // estado inicial dos botões
            UpdateAttachmentButtons();
        }

        #endregion

        #region Placeholders / UI helpers

        /// <summary>
        /// Define um texto de exemplo (placeholder) no campo de número conforme o tipo selecionado.
        /// Usa API nativa EM_SETCUEBANNER para exibir o cue banner.
        /// </summary>
        private void UpdateNumeroPlaceholder()
        {
            // Assegura que o handle do controle exista antes de enviar a mensagem
            var _ = txtNumero.Handle;

            if (rbBoleto.Checked)
            {
                // Exemplo de 44 dígitos para boleto
                var exemploBoleto = "00190500954014481606906809350314337370000000100"; // 44 dígitos
                SendMessage(txtNumero.Handle, EM_SETCUEBANNER, (IntPtr)1, $"Ex.: {exemploBoleto}");
            }
            else
            {
                // Exemplo livre para nota fiscal
                SendMessage(txtNumero.Handle, EM_SETCUEBANNER, (IntPtr)1, "Ex.: NF-12345");
            }
        }

        /// <summary>
        /// Habilita/desabilita botões de abrir/excluir anexo conforme existência de seleção/pending
        /// </summary>
        private void UpdateAttachmentButtons()
        {
            var hasSelection = lstAnexos.SelectedIndex >= 0 || !string.IsNullOrEmpty(pendingAttachmentSourcePath);
            btnOpenAttachment.Enabled = hasSelection;
            btnDeleteAttachment.Enabled = hasSelection;
        }

        #endregion

        #region Handlers de anexos (abrir / excluir / selecionar)

        /// <summary>
        /// Handler do botão que abre o diálogo de seleção de arquivo (anexar).
        /// O arquivo selecionado fica em pendingAttachmentSourcePath até o salvar.
        /// </summary>
        private void BtnAnexar_Click(object? sender, EventArgs e)
        {
            // Exige que tipo seja selecionado antes de anexar
            if (!rbBoleto.Checked && !rbNota.Checked)
            {
                MessageBox.Show("Selecione o tipo (Boleto ou Nota Fiscal) antes de anexar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var ofd = new OpenFileDialog();
            ofd.Title = "Anexar Boleto/Nota";
            ofd.Filter = "PDF files (*.pdf)|*.pdf|Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pendingAttachmentSourcePath = ofd.FileName;

                // Exibe apenas o nome do arquivo na lista (UI)
                lstAnexos.Items.Clear();
                lstAnexos.Items.Add(Path.GetFileName(pendingAttachmentSourcePath));

                // Se havia anexo salvo, consideramos substituído pelo pending
                savedAttachmentAbsolutePath = null;

                UpdateAttachmentButtons();
            }
        }

        /// <summary>
        /// Duplo clique na lista de anexos: abre o arquivo (pendente ou salvo)
        /// </summary>
        private void LstAnexos_DoubleClick(object? sender, EventArgs e)
        {
            if (lstAnexos.SelectedItem == null) return;

            if (!string.IsNullOrEmpty(pendingAttachmentSourcePath))
            {
                OpenFileExternal(pendingAttachmentSourcePath!);
                return;
            }

            if (!string.IsNullOrEmpty(savedAttachmentAbsolutePath))
            {
                OpenFileExternal(savedAttachmentAbsolutePath);
            }
        }

        /// <summary>
        /// Abre anexo pendente ou salvo utilizando o aplicativo padrão do sistema.
        /// </summary>
        private void BtnOpenAttachment_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pendingAttachmentSourcePath) && File.Exists(pendingAttachmentSourcePath))
            {
                OpenFileExternal(pendingAttachmentSourcePath);
                return;
            }

            if (!string.IsNullOrEmpty(savedAttachmentAbsolutePath) && File.Exists(savedAttachmentAbsolutePath))
            {
                OpenFileExternal(savedAttachmentAbsolutePath);
                return;
            }

            MessageBox.Show("Nenhum anexo disponível para abrir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Exclui anexo pendente (limpa seleção) ou exclui o arquivo salvo do disco quando possível.
        /// </summary>
        private void BtnDeleteAttachment_Click(object? sender, EventArgs e)
        {
            // Limpa pending (não houve cópia para pasta de anexos)
            if (!string.IsNullOrEmpty(pendingAttachmentSourcePath))
            {
                pendingAttachmentSourcePath = null;
                lstAnexos.Items.Clear();
                UpdateAttachmentButtons();
                return;
            }

            // Se houver arquivo salvo, tenta excluir do disco (silenciosamente em caso de falha)
            if (!string.IsNullOrEmpty(savedAttachmentAbsolutePath))
            {
                try
                {
                    if (File.Exists(savedAttachmentAbsolutePath))
                        File.Delete(savedAttachmentAbsolutePath);
                }
                catch
                {
                    // Não propagar erro ao usuário: exclusão não é crítica
                }

                savedAttachmentAbsolutePath = null;
                lstAnexos.Items.Clear();
                UpdateAttachmentButtons();
            }
        }

        /// <summary>
        /// Abre caminho no aplicativo padrão do sistema (Process.Start com UseShellExecute).
        /// </summary>
        private void OpenFileExternal(string path)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Não foi possível abrir o anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Cadastro / Validações

        /// <summary>
        /// Handler do botão Cadastrar: valida campos e cria novo Item.
        /// Se houver anexo pendente, copia para pasta de anexos e anexa ao Item.
        /// </summary>
        private async void BtnCadastrar_Click(object? sender, EventArgs e)
        {
            // Desabilita o botão imediatamente para evitar cliques duplos
            btnCadastrar.Enabled = false;

            // Validações básicas
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("O campo Número é obrigatório.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                btnCadastrar.Enabled = true; // reabilita pois validação falhou
                return;
            }

            // Removida validação de 44 dígitos para boletos — campo agora é livre

            if (string.IsNullOrWhiteSpace(txtFornecedor.Text))
            {
                MessageBox.Show("O campo Fornecedor é obrigatório.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFornecedor.Focus();
                btnCadastrar.Enabled = true; // reabilita pois validação falhou
                return;
            }

            if (numValor.Value <= 0)
            {
                MessageBox.Show("O valor deve ser maior que zero.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numValor.Focus();
                btnCadastrar.Enabled = true; // reabilita pois validação falhou
                return;
            }

            if (cmbStatus.SelectedIndex < 0 || string.IsNullOrEmpty(cmbStatus.SelectedItem?.ToString()))
            {
                MessageBox.Show("Selecione um status.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                btnCadastrar.Enabled = true; // reabilita pois validação falhou
                return;
            }

            // Monta o objeto Item
            var novoItem = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Tipo = rbBoleto.Checked ? TipoItem.Boleto : TipoItem.Nota,
                Numero = txtNumero.Text,
                Fornecedor = txtFornecedor.Text,
                Valor = numValor.Value,
                Vencimento = dtpVencimento.Value.Date,
                Status = (StatusItem)Enum.Parse(typeof(StatusItem), cmbStatus.SelectedItem!.ToString()!)
            };

            // Copia anexo pendente para pasta de anexos e cria Attachment
            if (!string.IsNullOrEmpty(pendingAttachmentSourcePath) && File.Exists(pendingAttachmentSourcePath))
            {
                var source = pendingAttachmentSourcePath!;
                var destFileName = Path.GetFileName(source);
                var dest = dataService.GetAttachmentPath(destFileName);
                dest = dataService.GetUniqueAttachmentPath(dest);
                File.Copy(source, dest);

                var relative = Path.GetRelativePath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dest);

                var attachment = new Attachment
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = Path.GetFileName(dest),
                    RelativePath = relative,
                    CreatedAt = DateTime.Now
                };

                novoItem.Attachments.Add(attachment);

                // Notifica form pai sobre novo anexo
                AttachmentAdded?.Invoke(attachment);

                // Guarda caminho salvo para futuras operações (abrir / excluir)
                savedAttachmentAbsolutePath = dest;

                // Limpa pending
                pendingAttachmentSourcePath = null;
            }

            // Notifica form pai sobre novo item criado
            ItemCreated?.Invoke(novoItem);

            // A notificação visual (MessageBox) foi removida daqui para evitar duplicação
            // O formulário pai pode exibir feedback ao usuário ao receber o evento ItemCreated

            // Limpa automaticamente o formulário após o cadastro
            ClearForm();

            // Aguarda 1s antes de reabilitar o botão para evitar cliques duplos
            await Task.Delay(1000);
            btnCadastrar.Enabled = true;
        }

        #endregion

        #region API pública / utilitários

        public string Numero => txtNumero.Text;
        public string Fornecedor => txtFornecedor.Text;
        public decimal Valor => numValor.Value;
        public DateTime Vencimento => dtpVencimento.Value.Date;
        public int SelectedStatusIndex => cmbStatus.SelectedIndex;
        public string? SelectedStatusString => cmbStatus.SelectedItem?.ToString();
        public bool IsBoleto => rbBoleto.Checked;

        /// <summary>
        /// Limpa o formulário e o estado de anexos.
        /// </summary>
        public void ClearForm()
        {
            pendingAttachmentSourcePath = null;
            lstAnexos.Items.Clear();

            rbBoleto.Checked = true;
            txtNumero.Clear();
            txtFornecedor.Clear();
            numValor.Value = 0;
            dtpVencimento.Value = DateTime.Now;
            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0;
            txtNumero.Focus();

            // Atualiza placeholder e limpa caminho salvo
            UpdateNumeroPlaceholder();
            savedAttachmentAbsolutePath = null;

            UpdateAttachmentButtons();
        }

        /// <summary>
        /// Tenta remover arquivo de anexo baseado em uma Attachment (usa RelativePath).
        /// Operação silenciosa em caso de falha.
        /// </summary>
        private void TryDeleteAttachmentFile(Attachment attachment)
        {
            try
            {
                var absolute = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), attachment.RelativePath);
                if (File.Exists(absolute))
                    File.Delete(absolute);
            }
            catch
            {
                // Ignora falhas de exclusão
            }
        }

        #endregion
    }
}
