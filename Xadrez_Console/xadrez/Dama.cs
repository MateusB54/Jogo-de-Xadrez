

using tabuleiro;

namespace xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "D";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            // para cima
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }

            // para direita
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            // para baixo
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }

            // para esquerda
            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }
            // para ne
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                while (Tab.posicaoValida(pos) && podeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Linha = pos.Linha - 1;
                    pos.Coluna = pos.Coluna + 1;
                }
            }

            // para se
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                while (Tab.posicaoValida(pos) && podeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Linha = pos.Linha + 1;
                    pos.Coluna = pos.Coluna + 1;
                }
            }

            // para so
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                while (Tab.posicaoValida(pos) && podeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Linha = pos.Linha + 1;
                    pos.Coluna = pos.Coluna - 1;
                }
            }

            // para no
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                while (Tab.posicaoValida(pos) && podeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Linha = pos.Linha - 1;
                    pos.Coluna = pos.Coluna - 1;
                }
            }

            return mat;

        }
    }
}
