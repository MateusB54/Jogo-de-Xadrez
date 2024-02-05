using System;
using tabuleiro;

namespace Xadrez_Console
{
    class program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8,8);

            Tela.ImprimirTabuleiro(tab);
        }
    }
}

