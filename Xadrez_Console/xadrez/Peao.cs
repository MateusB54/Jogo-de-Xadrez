
using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez Partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca peca = Tab.peca(pos);
            return peca != null && peca.Cor != Cor;
        }

        private bool estaLivre(Posicao pos)
        {
            return Tab.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.posicaoValida(pos) && estaLivre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(Posicao.Linha-2,Posicao.Coluna);
                if (Tab.posicaoValida(pos) && estaLivre(pos) && QtdMovimentos==0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(Posicao.Linha - 1, Posicao.Coluna-1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(Posicao.Linha - 1, Posicao.Coluna +1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Joga especial en passant
                if (Posicao.Linha==3)
                {
                    Posicao Esquerda = new Posicao(Posicao.Linha, Posicao.Coluna -1);
                    if (Tab.posicaoValida(Esquerda) && existeInimigo(Esquerda) && Tab.peca(Esquerda)== Partida.PecaVulneravel)
                    {
                        mat[Esquerda.Linha - 1, Esquerda.Coluna]= true;
                    }
                    
                    Posicao Direita = new Posicao(Posicao.Linha, Posicao.Coluna +1);
                    if (Tab.posicaoValida(Direita) && existeInimigo(Direita) && Tab.peca(Direita)== Partida.PecaVulneravel)
                    {
                        mat[Direita.Linha - 1, Direita.Coluna]= true;
                    }
                }

              
            }
            else
            {
                pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.posicaoValida(pos) && estaLivre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tab.posicaoValida(pos) && estaLivre(pos) && QtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Joga especial en passant
                if (Posicao.Linha == 4)
                {
                    Posicao Esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.posicaoValida(Esquerda) && existeInimigo(Esquerda) && Tab.peca(Esquerda) == Partida.PecaVulneravel)
                    {
                        mat[Esquerda.Linha+1, Esquerda.Coluna] = true;
                    }

                    Posicao Direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.posicaoValida(Direita) && existeInimigo(Direita) && Tab.peca(Direita) == Partida.PecaVulneravel)
                    {
                        mat[Direita.Linha+1, Direita.Coluna] = true;
                    }
                }
            }
            return mat;
        }
    }
}
