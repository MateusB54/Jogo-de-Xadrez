using System;
using tabuleiro;
using xadrez;

namespace Xadrez_Console
{
    class program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();


                while (!partida.Terminada)
                {
                    try {
                        Console.Clear();
                        Tela.imprimirPartida(partida);
                        

                        Console.WriteLine();
                        Console.Write("Posição da peça: ");
                        Posicao Origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(Origem);


                        bool[,] posicoesPossiveis = partida.Tab.peca(Origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Posição de destino: ");
                        Posicao Destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(Origem, Destino);
                        partida.realizaJogada(Origem, Destino);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }



                }

                
            }

            catch (TabuleiroException e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

