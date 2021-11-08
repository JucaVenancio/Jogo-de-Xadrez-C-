using tabuleiro;

namespace Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "C";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MatrizBool = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Nor-Nordeste
            pos.DefinirValores(posicao.linha - 2, posicao.coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Nor-Noroeste
            pos.DefinirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //És-Noroeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 2);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //És-Sudeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 2);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Su-Sudeste
            pos.DefinirValores(posicao.linha + 2, posicao.coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Su-Sudoeste
            pos.DefinirValores(posicao.linha + 2, posicao.coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Oés-Sudoeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 2);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            //Oés-Noroeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 2);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
            }

            return MatrizBool;

        }
    }
}
