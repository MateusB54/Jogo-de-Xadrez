﻿using tabuleiro;
using System.Collections.Generic;

namespace xadrez
{
     class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public Peca PecaVulneravel { get; private set; }
        public bool Terminada { get; private set; }
        public bool Xeque { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        
        

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada= false;
            PecaVulneravel = null;
            Xeque= false;
            Pecas= new HashSet<Peca>();
            Capturadas= new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executarMovimento( Posicao origem, Posicao destino)
        {
            Peca p= Tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca PecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);

            if (PecaCapturada != null) 
            { 
                Capturadas.Add(PecaCapturada);
            }

            // Jogada roque pequeno
            if (p is Rei && destino.Coluna== origem.Coluna + 2)
            {
                Posicao origemT = new Posicao (origem.Linha, origem.Coluna+3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                Tab.colocarPeca(T, destinoT);
            }

            // Jogada roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna -2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                Tab.colocarPeca(T, destinoT);
            }

            // Jogada en pasant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && PecaCapturada==null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Preta)
                    {
                        posP= new Posicao (destino.Linha - 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    PecaCapturada=Tab.retirarPeca(posP);
                    Capturadas.Add(PecaCapturada);
                }
            }

            return PecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if (pecaCapturada!= null) 
            {
                Tab.colocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p, origem);

            //  Jogada roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                Tab.colocarPeca(T, origemT);
            }
            // Jogada roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                Tab.colocarPeca(T, origemT);
            }

            //Jogada en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == PecaVulneravel)
                {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP= new Posicao (3,destino.Coluna);
                    }
                    else
                    {
                        posP =new Posicao(4,destino.Coluna);
                    }
                    Tab.colocarPeca (peao, posP);
                }
            }
        }

        public void realizaJogada (Posicao origem, Posicao destino)
        {
            Peca PecaCapturada = executarMovimento(origem, destino);

            if (estaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, PecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = Tab.peca(destino);

            //Jogada Especial promoção
            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha==0) || (p.Cor == Cor.Preta && destino.Linha==7))
                {
                    p = Tab.retirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(Tab, p.Cor);
                    Tab.colocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversario(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque= false;
            }

            if (xequeMate(adversario(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                mudaJogador();
            }

            

            // Jogada en passant
            
            if (p is Peao && (destino.Linha == origem.Linha + 2 || destino.Linha == origem.Linha - 2))
            {
                PecaVulneravel = p;
            }
            else
            {
                PecaVulneravel = null;
            }

        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição escolhida!");
            }
            if (JogadorAtual !=Tab.peca(pos).Cor) 
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("A peça escolhida não possui movimentos possíveis!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual= Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        private Cor adversario (Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else 
            {
                return Cor.Branca;
            }
        }

        private Peca rei (Cor cor)
        {
            foreach(Peca p in pecasEmJogo(cor))
            {
                if (p is Rei)
                {
                    return p;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não há um rei da cor " + cor + " no tabuleiro");
            }
            foreach (Peca p in pecasEmJogo(adversario(cor)))
            {
                bool[,] mat = p.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool xequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca p in pecasEmJogo(cor))
            {
                bool[,] mat = p.movimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    Console.Write(8 - i + " ");
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao Origem = p.Posicao; 
                            Posicao Destino = new Posicao(i, j);
                            Peca PecaCapturada = executarMovimento(Origem, Destino);
                            bool TesteXeque = estaEmXeque(cor);
                            desfazMovimento(Origem, Destino, PecaCapturada);
                            if (!TesteXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in Capturadas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in Pecas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));  
            colocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));



            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));

        }
    }
}
