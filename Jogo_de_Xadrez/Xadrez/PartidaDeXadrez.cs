using System.Collections.Generic;
using tabuleiro;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public  int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public bool  Xeque { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tabuleiro.RetirarPeca(origem);
            peca.IncrementarQteMovimentos();
            Peca PecaCapturada = tabuleiro.RetirarPeca(destino);
            tabuleiro.ColocarPeca(peca, destino);
            if(PecaCapturada != null)
            {
                capturadas.Add(PecaCapturada);
            }

            //#Jogada Especial Roque Pequeno
            if(peca is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao DestinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tabuleiro.RetirarPeca(OrigemT);
                T.IncrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, DestinoT);
            }

            //#Jogada Especial Roque Grande
            if (peca is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao DestinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tabuleiro.RetirarPeca(OrigemT);
                T.IncrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, DestinoT);
            }

            //#Jogada Especial En Passant
            if(peca is Peao)
            {
                if(origem.coluna != destino.coluna && PecaCapturada == null)
                {
                    Posicao PosP;
                    if(peca.cor  == Cor.Branco)
                    {
                        PosP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        PosP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    PecaCapturada = tabuleiro.RetirarPeca(PosP);
                    capturadas.Add(PecaCapturada);
                }
            }

            return PecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = tabuleiro.RetirarPeca(destino);
            peca.DecrementarQteMovimentos();
            if(pecaCapturada != null)
            {
                tabuleiro.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);                
            }
            tabuleiro.ColocarPeca(peca, origem);

            //#Jogada Especial Roque Pequeno
            if (peca is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao DestinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tabuleiro.RetirarPeca(DestinoT);
                T.DecrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, OrigemT);
            }

            //#Jogada Especial Roque Grande
            if (peca is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao DestinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tabuleiro.RetirarPeca(DestinoT);
                T.DecrementarQteMovimentos();
                tabuleiro.ColocarPeca(T, OrigemT);
            }

            //#Jogada Especial En Passant
            if(peca is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca Peao = tabuleiro.RetirarPeca(destino);
                    Posicao posP;
                    if(Peao.cor == Cor.Branco)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }

                    tabuleiro.ColocarPeca(Peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }

            if (EstaEmXeque(CorAdversaria(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TestaXequeMate(CorAdversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {

                turno++;
                MudaJogador();
            }

            Peca p = tabuleiro.Peca(destino);

            //#Jogada Especial En Passant
            if(p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }

        public void MudaJogador()
        {
            if(jogadorAtual == Cor.Branco)
            {
                jogadorAtual = Cor.Preto;
            }
            else
            {
                jogadorAtual = Cor.Branco;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }

            return aux;

        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
                
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;

        }

        private Cor CorAdversaria(Cor cor)
        {
            if(cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }

        }

        private Peca Rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if(R == null )
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro");
            }

            foreach (Peca x in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] Mat = x.MovimentosPossiveis();

                if (Mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TestaXequeMate(Cor cor)
        {
            if(!EstaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca  x in PecasEmJogo(cor))
            {
                bool[,] Mat = x.MovimentosPossiveis();

                for(int i = 0; i < tabuleiro.Linhas; i++)
                {
                    for(int j = 0; j < tabuleiro.Colunas; j++)
                    {
                        if(Mat[i,j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i,j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testaXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if(!testaXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(tabuleiro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça nessa posição!");
            }
            if(jogadorAtual != tabuleiro.Peca(pos).cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if(!tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!tabuleiro.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branco, this));


            ColocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Preto));
            ColocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preto, this));

        }
    }
}
