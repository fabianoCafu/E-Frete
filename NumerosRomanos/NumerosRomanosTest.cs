using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using NUnit.Framework;
using System;

namespace Test.NumerosRomanos
{
    public class NumerosRomanosTest
    {
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("IV", 4)]
        [TestCase("V", 5)]
        [TestCase("VI", 6)]
        [TestCase("VII", 7)]
        [TestCase("VIII", 8)]
        [TestCase("IX", 9)]
        [TestCase("X", 10)]
        [TestCase("XI", 11)]
        [TestCase("XII", 12)]
        [TestCase("XIII", 13)]
        [TestCase("XIV", 14)]
        [TestCase("XV", 15)]
        [TestCase("XVI", 16)]
        [TestCase("XVII", 17)]
        [TestCase("XVIII", 18)]
        [TestCase("XIX", 19)]
        [TestCase("XX", 20)]
        [TestCase("XXX", 30)]
        [TestCase("XL", 40)]
        [TestCase("L", 50)]
        [TestCase("LVIII", 58)]
        [TestCase("C", 100)]
        [TestCase("CC", 200)]
        [TestCase("CCC", 300)]
        [TestCase("CD", 400)]
        [TestCase("D", 500)]
        [TestCase("M", 1000)]

        [TestCase("MCMXC", 1990)]
        [TestCase("MCMXCI", 1991)]
        [TestCase("MCMXCII", 1992)]
        [TestCase("MCMXCIII", 1993)]
        [TestCase("MCMXCIV", 1994)]
        [TestCase("MCMXCV", 1995)]

        public void Teste(string numeroRomano, int numeroInteiroEsperado)
        {
            //Arrange

            //Act
            var resultado = NumeroRomanoParaInteiro(numeroRomano);

            //Assert
            Assert.AreEqual(numeroInteiroEsperado, resultado);
        }

        #region * Enunciado *

        /*
        Numeros romanos são repreentados por 7 diferentes simbolos: I, V, X, L, C, D e M.

        Simbolo     ValorValue
        I ->         1
        V            5
        X ->         10
        L            50
        C ->         100
        D            500
        M            1000

        Por exemplo, 2 é escrito II em romando, apenas dois 1's e somam-se. 12 é escrito XII, onde é simplesmente X(10) + II(2).
        O número 27 é escrito XXVII, onde XX(20) + V(5) + II(2)
        
        Numeros romanos são geralmente escritos do mais alto para o mais baixo da esquerda para a direita
        Porém o número 4 não é IIII, e sim IV. Desta maneira, o I(1) é menor que V(5), o que gera 5-1, que é 4

        O mesmo princípio se aplica ao número 9, que é escrito IX
        
        Há seis intancias onde a subtração é usada:
            I pode ser colocado antes de um V (5) e X (10) para gerar 4 e 9. 
            X pode ser colocado antes de um L (50) e C (100) para gerar 40 e 90. 
            C pode ser colocado antes de um D (500) e M (1000) para gerar 400 e 900.
            
        Visto isto, crie um método que converta um numero romano num inteiro

            Exemplo 1:
            Input: s = "III"
            Output: 3
            Explicação: III = 3.

            Exemplo 2:
            Input: s = "LVIII"
            Output: 58
            Explicação: L = 50, V= 5, III = 3.

            Exemplo 3:
            Input: s = "MCMXCIV"
            Output: 1994
            Explicação: M = 1000, CM = 900, XC = 90 and IV = 4. 

        Restrições:
            O tamanho do número romano enviado 1 <= s.length <= 15
            s contém apenas os caracteres a seguir ('I', 'V', 'X', 'L', 'C', 'D', 'M').
            É garantido que somente serão enviados números romanos no seguinte intervalo [1, 3999].
        */

        #endregion

        private int NumeroRomanoParaInteiro(string numeroRomano)
        {
            var numeroConvertido = 0;
            var totalLetras = numeroRomano.Length;

            if (!string.IsNullOrWhiteSpace(numeroRomano)
                && totalLetras >= 1 && totalLetras <= 15)
            {
                numeroConvertido = totalLetras > 1
                    ? ValidaNumeroValido(numeroRomano)
                    : ConverteNumero(numeroRomano);
            }

            return numeroConvertido;
        }

        private int ValidaNumeroValido(string numeroRomano)
        {
            var numeroConvertido = 0;
            var totalLetras = numeroRomano.Length - 1;

            for (var indice = 0; indice <= totalLetras; indice++)
            {
                if (indice > 0)
                {
                    var anterior = numeroRomano[indice - 1].ToString().ToUpper();
                    var atual = numeroRomano[indice].ToString().ToUpper();
                    var proximo = indice < totalLetras
                        ? numeroRomano[indice + 1].ToString().ToUpper()
                        : atual;

                    if (atual.Contains("C") && (proximo.Contains("M") || proximo.Contains("D"))
                        || atual.Contains("X") && (proximo.Contains("C") || proximo.Contains("L"))
                        || atual.Contains("I") && (proximo.Contains("V") || proximo.Contains("X")))
                    {
                        numeroConvertido += ConverteNumero(proximo) - ConverteNumero(atual);
                        indice++;
                    }
                    else if ((atual.Contains("V") || atual.Contains("X")) && anterior.Contains("I")
                        || (atual.Contains("D") || atual.Contains("M")) && anterior.Contains("C")
                        || atual.Contains("L") || atual.Contains("C") && anterior.Contains("X"))
                    {
                        numeroConvertido = ConverteNumero(atual) - ConverteNumero(anterior);
                        indice++;
                    }
                    else
                    {
                        numeroConvertido = ValidaConvercao(atual, anterior, proximo, numeroConvertido);
                    }
                }
                else
                {
                    numeroConvertido += ConverteNumero(numeroRomano[indice].ToString());
                }
            }

            return numeroConvertido;
        }

        private int ValidaConvercao(
            string atual,
            string anterior,
            string proximo,
            int numeroConvertido)
        {
            if (atual.Contains("I"))
            {
                if (anterior.Contains("V") || anterior.Contains("X")
                    || proximo.Contains("I"))
                {
                    numeroConvertido += ConverteNumero(atual);
                }
            }
            else if (atual.Contains("X"))
            {
                if (anterior.Contains("L") || anterior.Contains("C")
                    || proximo.Contains("X"))
                {
                    numeroConvertido += ConverteNumero(atual);
                }
            }
            else if (atual.Contains("C"))
            {
                if (anterior.Contains("D") || anterior.Contains("M")
                    || proximo.Contains("C"))
                {
                    numeroConvertido += ConverteNumero(atual);
                }
            }
            else
            {
                numeroConvertido += ConverteNumero(atual);
            }

            return numeroConvertido;
        }

        private int ConverteNumero(string numero)
        {
            var numeroConvertido = 0;

            switch (numero.ToUpper())
            {
                case "I":
                    numeroConvertido = 1;
                    break;
                case "V":
                    numeroConvertido = 5;
                    break;
                case "X":
                    numeroConvertido = 10;
                    break;
                case "L":
                    numeroConvertido = 50;
                    break;
                case "C":
                    numeroConvertido = 100;
                    break;
                case "D":
                    numeroConvertido = 500;
                    break;
                case "M":
                    numeroConvertido = 1000;
                    break;
            }

            return numeroConvertido;
        }
    }
}