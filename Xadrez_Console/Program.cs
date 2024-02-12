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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.Tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao Origem = Tela.lerPosicaoXadrez().toPosicao();

                    bool[,] posicoesPossiveis =partida.Tab.peca(Origem).movimentosPossiveis();

                    Console.Clear() ;
                    Tela.imprimirTabuleiro(partida.Tab, posicoesPossiveis) ;

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao Destino = Tela.lerPosicaoXadrez().toPosicao();
                    partida.executarMovimento (Origem, Destino);


                }

                
            }

            catch (TabuleiroException e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

