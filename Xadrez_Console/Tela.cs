using System;
using tabuleiro;

namespace Xadrez_Console
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i=1; i<tab.Linhas; i++)
            {
                for (int j=1; j<tab.Colunas; j++)
                {
                    if (tab.peca(i,j)==null)
                    {
                        Console.Write("- "); 
                    }
                    else
                    {
                        Console.Write(tab.peca(i, j) + " ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
       
    }
}
