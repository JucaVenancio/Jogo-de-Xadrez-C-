using tabuleiro;

namespace Xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "D";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MatrizBool = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Norte
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            if (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
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

            //Norte
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
            }

            //Leste
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.coluna = pos.coluna + 1;
            }

            //Sul
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha + 1;
            }

            //Oeste
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            while (tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrizBool[pos.linha, pos.coluna] = true;
                if (tabuleiro.Peca(pos) != null && tabuleiro.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }

            return MatrizBool;

        }
    }
}
