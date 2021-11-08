using tabuleiro;

namespace Xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MatrizBool = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Noroeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
                pos.coluna = pos.coluna - 1;
            }

            //Nordeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
                pos.coluna = pos.coluna + 1;
            }

            //Sudeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha + 1;
                pos.coluna = pos.coluna + 1;
            }

            //Sudoeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha + 1;
                pos.coluna = pos.coluna - 1;
            }

            return MatrizBool;

        }
    }
}
