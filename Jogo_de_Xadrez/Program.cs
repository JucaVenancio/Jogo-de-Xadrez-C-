using System;
using tabuleiro;
using Xadrez;

namespace Jogo_de_Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

                while (!partidaDeXadrez.Terminada)
                {
                    try
                    {
                        Tela.ImprimirPartida(partidaDeXadrez);

                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao Origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partidaDeXadrez.ValidarPosicaoDeOrigem(Origem);

                        bool[,] MovimentosPossiveis = partidaDeXadrez.tabuleiro.Peca(Origem).MovimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidaDeXadrez.tabuleiro, MovimentosPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao Destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partidaDeXadrez.ValidarPosicaoDeDestino(Origem, Destino);

                        partidaDeXadrez.RealizaJogada(Origem, Destino);
                    }catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            Console.Clear();
                Tela.ImprimirPartida(partidaDeXadrez);
           
            Console.ReadKey();
            
        }
    }
}
