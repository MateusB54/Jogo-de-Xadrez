using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez Partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p =Tab.peca(pos);
             return p != null && p.Cor == Cor && p is Torre && p.QtdMovimentos==0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas,Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            // para cima
            pos.definirValores(Posicao.Linha -1 , Posicao.Coluna);
            if (Tab.posicaoValida(pos) && podeMover(pos) )
            {
                mat[pos.Linha,pos.Coluna] = true;   
            }

            // para ne
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna+1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // para direita
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // para se
            pos.definirValores(Posicao.Linha+1, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // para baixo
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // para so
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna-1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // para esquerda
            pos.definirValores(Posicao.Linha , Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // para no
            pos.definirValores(Posicao.Linha-1, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Implementação de jogada especial

            if (QtdMovimentos==0 && !Partida.Xeque)
            {
                // Roque pequeno
                Posicao posT1= new Posicao (Posicao.Linha, Posicao.Coluna+3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tab.peca(p1)==null && Tab.peca(p2)==null)
                    {
                        mat[Posicao.Linha,Posicao.Coluna+2] = true;
                    }
                }
                // Roque grande
                Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna -4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3)==null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }

            }

            return mat;

        }

        
    }
}
