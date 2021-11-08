using tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool TesteParaRoque(Posicao pos)
        {
            Peca p = tabuleiro.Peca(pos);
            return p != null && p is Torre && p.cor == cor && p.QteMovimentos == 0;
        }
        
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MatrizBool = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Norte
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            if(tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Nordeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Leste
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Sudeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Sul
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Sudoeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Oeste
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Noroeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //#Jogada Especial (ROQUE)
            if(QteMovimentos == 0 && !partida.Xeque)
            {
                //Roque Pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if(TesteParaRoque(posT1))
                {
                    Posicao P1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao P2 = new Posicao(posicao.linha, posicao.coluna + 2);

                    if(tabuleiro.Peca(P1) == null && tabuleiro.Peca(P2) == null)
                    {
                        MatrizBool[posicao.linha, posicao.coluna + 2] = true;
                    }
                }

                //Roque Grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (TesteParaRoque(posT1))
                {
                    Posicao P1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao P2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao P3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (tabuleiro.Peca(P1) == null && tabuleiro.Peca(P2) == null && tabuleiro.Peca(P3) == null)
                    {
                        MatrizBool[posicao.linha, posicao.coluna - 2] = true;
                    }
                }

            }



            return MatrizBool;

        }
    }
}
