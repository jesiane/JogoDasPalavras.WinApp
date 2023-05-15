namespace JogoDasPalavras.WinApp
{
    public partial class Form1 : Form
    {
        private JogoPalavras jogoPalavras;
        private int pCount;
        private int lCount;
        private Button[] letrasDigitados;


        public Form1()
        {

            InitializeComponent();
            this.jogoPalavras = new JogoPalavras();
            Resetar();
            ConfigurarBotoes();
        }

        public void ConfigurarBotoes()
        {
            foreach (Button botao in panelBotoes.Controls)
            {
                botao.Click += EscolherLetra;
            }
            btnStart.Click += ResetarEvento;
        }

        public void EscolherLetra(object? sender, EventArgs e)
        {
            Button botao = (Button)sender;
            if (botao.Text == "Enter")
            {
                EnviarPalpite();
                return;
            }
            lCount = lCount < 5 ? ++lCount : 5;
            Label letra = (Label)panPalavras.Controls[25 - (pCount + lCount)];
            letra.Text = botao.Text;
            letrasDigitados[lCount - 1] = botao;
        }

        public void EnviarPalpite()
        {
            if (lCount < 5)
            {
                return;
            }
            string palavra = VerificarPalpite();
            MostrarFeedback(palavra);
            AtualizarPalavra();
            VerificarResultado();
        }

        public string VerificarPalpite()
        {
            string palpite = "";
            for (int i = 0; i < 5; i++)
            {
                Label letra = (Label)panPalavras.Controls[24 - (pCount + i)];
                palpite += letra.Text;
            }
            return jogoPalavras.VerificacaoPalavra(palpite);
        }

        public void MostrarFeedback(string palavra)
        {
            for (int i = 0; i < 5; i++)
            {
                Color cor;
                switch (palavra[i])
                {
                    case 'T':
                        cor = Color.Green;
                        break;
                    case 'C':
                        cor = Color.Yellow;
                        break;
                    default:
                        cor = Color.White;
                        letrasDigitados[i].Enabled = false;
                        letrasDigitados[i].BackColor = this.BackColor;
                        break;
                }
                Label letra = (Label)panPalavras.Controls[24 - (pCount + i)];
                letra.BackColor = cor;
            }
        }

        public void VerificarResultado()
        {
            string resultado = jogoPalavras.ObterResultado();
            if (resultado != "")
            {
                panelBotoes.Enabled = false;
                lblResultado.Text = resultado;
                MessageBox.Show(resultado);
            }
        }

        private void AtualizarPalavra()
        {
            pCount += 5;
            lCount = 0;
            letrasDigitados = new Button[5];
        }

        private void Resetar()
        {
            this.jogoPalavras.IniciarValores();
            this.letrasDigitados = new Button[5];
            this.panelBotoes.Enabled = true;
            this.lblResultado.Text = "Digite uma letra";
            this.pCount = 0;
            this.lCount = 0;
            ResetarPanels();
        }

        private void ResetarPanels()
        {
            foreach (Button botao in this.panelBotoes.Controls)
            {
                botao.Enabled = true;
                botao.BackColor = this.btnEnter.BackColor;
            }
            foreach (Label letra in this.panPalavras.Controls)
            {
                letra.Text = "";
                letra.BackColor = this.BackColor;
            }
        }

        

        private void btnResetar_Click(object sender, EventArgs e)
        {

        }
    }
}