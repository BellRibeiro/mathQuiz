using Microsoft.Maui.Dispatching;
using System.Timers;

namespace ProjetoMatematico
{
    public partial class MainPage : ContentPage
    {

        int pontuacao = 0; // Inicia a pontucao com 0
        int nivelDificuldade = 1; //Inicia a dificuldade com 1
        int respostaCorreta; // Cria variavel  resposta
        int erros = 0; // Iniciar com 0 erros
        Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
            GerarNovaOperacao();
        }


        void AtualizarDificuldade()
        {
            if (pontuacao >= 10) nivelDificuldade = 3;
            if (pontuacao >= 20) nivelDificuldade = 4;
            if (pontuacao >= 30) nivelDificuldade = 5; 
            if (pontuacao >= 40) nivelDificuldade = 6; 
            if (pontuacao >= 50) nivelDificuldade = 7; 
        }

        void GerarNovaOperacao()
        {
            
            int num1 = random.Next(1, 10 * nivelDificuldade);
            int num2 = random.Next(1, 10 * nivelDificuldade);

            if (num1 < 0)
            {
                num1 += 1;
            }

            
            if (nivelDificuldade <= 2)
            {
                respostaCorreta = num1 + num2;
                LabelOperacao.Text = $"{num1} + {num2}";
            }
            else if (nivelDificuldade <= 4)
            {
                respostaCorreta = num1 - num2;
                LabelOperacao.Text = $"{num1} - {num2}";
            }
            else if (nivelDificuldade <= 6)
            {
                respostaCorreta = num1 * num2;
                LabelOperacao.Text = $"{num1} × {num2}";
            }
            else
            {
                
                if (num2 != 0) respostaCorreta = num1 / num2; 
                else respostaCorreta = num1; 
                LabelOperacao.Text = $"{num1} ÷ {num2}";
            }

            
            int[] respostas = new int[4];
            int posicaoCorreta = random.Next(0, 4);
            respostas[posicaoCorreta] = respostaCorreta;

            for (int i = 0; i < 4; i++)
            {
                if (i != posicaoCorreta)
                {
                    respostas[i] = respostaCorreta + random.Next(-10, 10);
                    if (respostas[i] == respostaCorreta)
                    {
                        respostas[i] += random.Next(1, 3);
                    }
                }
            }

           
            BotaoResposta1.Text = respostas[0].ToString();
            BotaoResposta2.Text = respostas[1].ToString();
            BotaoResposta3.Text = respostas[2].ToString();
            BotaoResposta4.Text = respostas[3].ToString();
        }

        
        void AoClicarBotaoResposta(object sender, EventArgs e)
            {
                Button botaoClicado = (Button)sender;
                int respostaUsuario = int.Parse(botaoClicado.Text);

                if (respostaUsuario == respostaCorreta)
                {
                    pontuacao++;
                    LabelFeedback.Text = "Correto!";
                    LabelFeedback.TextColor = Colors.Green;
                }

                if (respostaUsuario != respostaCorreta)
                {
                    erros++;
                    LabelFeedback.Text = "Incorreto!";
                    LabelFeedback.TextColor = Colors.Red;

                    if (erros >= 3) {
                        LabelFeedback.Text = "Você atingiu o limite de erros.";
                        DesabilitarBotoes();
                }
                }

                // Atualiza pontuação, erros e dificuldade
                LabelPontuacao.Text = $"Pontuação: {pontuacao}";
                LabelErros.Text = $"Erros: {erros}/3";
                AtualizarDificuldade();

                // Gera uma nova operação
                GerarNovaOperacao();
            }

            // Desabilita os botões de resposta quando o jogo termina
            void DesabilitarBotoes()
            {
                BotaoResposta1.IsEnabled = false;
                BotaoResposta2.IsEnabled = false;
                BotaoResposta3.IsEnabled = false;
                BotaoResposta4.IsEnabled = false;
                BotaoRecomecar.IsEnabled = true;
            }

            void AoClicarBotaoRecomecar(object sender, EventArgs e)
            {
                pontuacao = 0;
                erros = 0;
                nivelDificuldade = 1;

                LabelPontuacao.Text = "Pontuação: 0";
                LabelErros.Text = "Erros: 0/3";
                LabelFeedback.Text = "";

                BotaoResposta1.IsEnabled = true;
                BotaoResposta2.IsEnabled = true;
                BotaoResposta3.IsEnabled = true;
                BotaoResposta4.IsEnabled = true;

                BotaoRecomecar.IsEnabled = false; // Desabilita o botão de recomeçar
                GerarNovaOperacao(); // Gera uma nova operação para recomeçar o jogo
            }
        }
    }


