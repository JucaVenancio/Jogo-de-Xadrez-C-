using tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez partida;

        public  Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = tabuleiro.Peca(pos);
            return p != null && p.cor != cor;
        }

        private bool Livre(Posicao pos)
        {
            return tabuleiro.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MatrizBool = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branco)
            {
                pos.DefinirValores(posicao.linha - 1, posicao.coluna);
                if (tabuleiro.PosicaoValida(pos) && Livre(pos))
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha - 2, posicao.coluna);
                if (tabuleiro.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                //#Jogada Especial En Passant
                if (posicao.linha == 3)
                {
                    Posicao Left = new Posicao(posicao.linha, posicao.coluna - 1);

                    if (tabuleiro.PosicaoValida(Left) && ExisteInimigo(Left) && tabuleiro.Peca(Left) == partida.VulneravelEnPassant)
                    {
                        MatrizBool[Left.linha - 1, Left.coluna] = true;
                    }

                    Posicao Right = new Posicao(posicao.linha, posicao.coluna + 1);

                    if (tabuleiro.PosicaoValida(Right) && ExisteInimigo(Right) && tabuleiro.Peca(Right) == partida.VulneravelEnPassant)
                    {
                        MatrizBool[Right.linha - 1, Right.coluna] = true;
                    }
                }
            }
            else
            {
                pos.DefinirValores(posicao.linha + 1, posicao.coluna);
                if (tabuleiro.PosicaoValida(pos) && Livre(pos))
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha + 2, posicao.coluna);
                if (tabuleiro.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    MatrizBool[pos.linha, pos.coluna] = true;
                }

                //#Jogada Especial En Passant
                if (posicao.linha == 4)
                {
                    Posicao Left = new Posicao(posicao.linha, posicao.coluna - 1);

                    if (tabuleiro.PosicaoValida(Left) && ExisteInimigo(Left) && tabuleiro.Peca(Left) == partida.VulneravelEnPassant)
                    {
                        MatrizBool[Left.linha + 1, Left.coluna] = true;
                    }

                    Posicao Right = new Posicao(posicao.linha, posicao.coluna + 1);

                    if (tabuleiro.PosicaoValida(Right) && ExisteInimigo(Right) && tabuleiro.Peca(Right) == partida.VulneravelEnPassant)
                    {
                        MatrizBool[Right.linha + 1, Right.coluna] = true;
                    }
                }


            }
            return MatrizBool;

        }
    }
}
