using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoritmoGenetico
{
    internal class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Alocação de tarefas");
            Console.ResetColor();
            Console.WriteLine();

            List<List<Trabalhador>> populacao = new List<List<Trabalhador>>();
            var avaliacao = new List<int>();

            //Cromosso
            var nomesTrabalhadores = new List<string>
            {
                "Steve", "Robert", "Susan", "Greg", "Austin", "Joe", "Frank", "Abu", "Kelly", "Michael"
            };
            var tarefas = AtribuirTarefas();

            for (int i = 0; i < 10; i++)
            {
                //Inicializar população
                var individuo = InicializarPopulacao(nomesTrabalhadores, tarefas);
                populacao.Add(individuo);

                // Exibir os indivíduos em uma tabela
                Console.WriteLine($"Indíviduo {i + 1}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Nome\t\tTarefa\tNota");
                Console.ResetColor();

                foreach (var t in individuo)
                {
                    Console.WriteLine($"{t.Nome}\t\t{t.Tarefa}\t{t.Nota}");
                }

                //Avaliar população
                var resultadoAvaliacao = AvaliarPopulacao(individuo);

                Console.WriteLine($"Resultado da avaliação do indivíduo {i + 1}: {resultadoAvaliacao}");
                Console.WriteLine("-----------------------------");

                avaliacao.Add(resultadoAvaliacao);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Iniciando reprodução...");
            Console.ResetColor();

            Console.WriteLine($"Selecionando pais...");

            var paiIndex = random.Next(populacao.Count);
            var maeIndex = random.Next(populacao.Count);

            //Certificar que pai e mãe sejam diferentes
            while (maeIndex == paiIndex)
            {
                maeIndex = random.Next(populacao.Count);
            }

            Console.WriteLine($"Pais selecionados: ID Pai: {paiIndex + 1}, ID Mae: {maeIndex + 1} ");

            #region Elitismo
            var pai = populacao[paiIndex];
            var mae = populacao[maeIndex];

            var metadePaiUm = pai.GetRange(0, 5);
            var metadeMaeUm = mae.GetRange(5, 5);

            var cruzamentoUm = new List<Trabalhador>();
            cruzamentoUm.AddRange(metadePaiUm);
            cruzamentoUm.AddRange(metadeMaeUm);

            var individuoUm = cruzamentoUm;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Nome\t\tTarefa\tNota");
            Console.ResetColor();
            foreach (var t in individuoUm)
            {
                Console.WriteLine($"{t.Nome}\t\t{t.Tarefa}\t{t.Nota}");
            }

            var cruzamentoDois = new List<Trabalhador>();
            cruzamentoDois.AddRange(metadeMaeUm);
            cruzamentoDois.AddRange(metadePaiUm);
            var individuoDois = cruzamentoDois;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Nome\t\tTarefa\tNota");
            Console.ResetColor();
            foreach (var t in individuoDois)
            {
                Console.WriteLine($"{t.Nome}\t\t{t.Tarefa}\t{t.Nota}");
            }

            #endregion


            #region Seleção melhor índivuduo
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSelecionando o melhor indivíduo...");

            var melhorIndividuo = SelecionarMelhorIndividuo(avaliacao, populacao);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Indivíduo selecionado: {populacao.IndexOf(melhorIndividuo) + 1}");
            Console.ResetColor();
            #endregion


            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n\tGeração 01");
            Console.ResetColor();
            List<List<Trabalhador>> primeiraGeracao = new List<List<Trabalhador>>();
            primeiraGeracao.Add(cruzamentoUm.Select(t => t.Clone()).ToList());
            primeiraGeracao.Add(cruzamentoDois.Select(t => t.Clone()).ToList());
            primeiraGeracao.Add(melhorIndividuo.Select(t => t.Clone()).ToList());
            primeiraGeracao.Add(melhorIndividuo.Select(t => t.Clone()).ToList());

            foreach (var lista in primeiraGeracao)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Nome\t\tTarefa\tNota");
                Console.ResetColor();
                foreach (var t in lista)
                {
                    Console.WriteLine($"{t.Nome}\t\t{t.Tarefa}\t{t.Nota}");

                }
            }

            //MUTAÇÃO
            var selecaoAleatoria = random.Next(0, 4);         
            var individuoSelecionado = primeiraGeracao[selecaoAleatoria];
            var tabalhadorIndex = random.Next(0, 10);

            var trab = individuoSelecionado[tabalhadorIndex];
            var notaAnterior = trab.Nota;

            Console.WriteLine($"SELECIONADO INDIVIDUO: {selecaoAleatoria}");
            Console.WriteLine($"TRABALHADOR: {trab.Nome} {trab.Tarefa} {trab.Nota}");

            trab.Nota = random.Next(1, 11);

            while(notaAnterior == trab.Nota)
                trab.Nota = random.Next(1, 11);

            Console.WriteLine($"NOTA APÓS MUTAÇÃO: {trab.Nota}");


            Console.WriteLine($"_____MUTACAO________________");

            foreach (var lista in primeiraGeracao)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Nome\t\tTarefa\tNota");
                Console.ResetColor();

                foreach (var t in lista)
                {
                    Console.WriteLine($"{t.Nome}\t\t{t.Tarefa}\t{t.Nota}");
                }
            }

            Console.ReadLine();
        }
        static List<Trabalhador> InicializarPopulacao(List<string> nomesTrabalhadores, List<int> tarefas)
        {
            var trabalhadores = new List<Trabalhador>();
            for (int i = 0; i <= 9; i++)
            {
                int notaAleatoria = random.Next(1, 11);

                var trab = new Trabalhador();
                trab.Nome = nomesTrabalhadores[i];
                trab.Tarefa = tarefas[i];
                trab.Nota = notaAleatoria;

                trabalhadores.Add(trab);
            }

            return trabalhadores;
        }
        static int AvaliarPopulacao(List<Trabalhador> trabalhadores)
        {
            return trabalhadores.Sum(trabalhador => trabalhador.Nota);
        }
        static List<int> AtribuirTarefas()
        {
            Random rand = new Random();

            var tarefas = new List<int>();

            var numerosUtilizados = new HashSet<int>();

            while (tarefas.Count < 10)
            {
                int tarefaAleatoria = rand.Next(1, 11);

                if (!numerosUtilizados.Contains(tarefaAleatoria))
                {
                    tarefas.Add(tarefaAleatoria);
                    numerosUtilizados.Add(tarefaAleatoria);
                }
            }

            return tarefas;
        }

        static List<Trabalhador> SelecionarMelhorIndividuo(List<int> avaliacao, List<List<Trabalhador>> populacao)
        {
            int maiorValor = avaliacao.Max();
            int indiceMaiorValor = avaliacao.IndexOf(maiorValor);
            var melhor = populacao[indiceMaiorValor];

            return melhor;
        }

    }
}
