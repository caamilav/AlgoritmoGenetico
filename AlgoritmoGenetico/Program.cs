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


            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Iniciando reprodução...");
            Console.ResetColor();

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"[ {i + 1} ] Selecionando pais...");

                var paiIndex = random.Next(populacao.Count);
                var maeIndex = random.Next(populacao.Count);

                //Certificar que pai e mãe sejam diferentes
                while (maeIndex == paiIndex)
                {
                    maeIndex = random.Next(populacao.Count);
                }

                Console.WriteLine($"[ {i + 1} ] Pais selecionados: ID Pai: {paiIndex + 1}, ID Mae: {maeIndex + 1} ");
                var pai = populacao[paiIndex];
                var mae = populacao[maeIndex];


                // Realize o cruzamento
                var metadePai = pai.GetRange(0, 5);
                var metadeMae = mae.GetRange(5, 5);

                List<Trabalhador> cruzamento = new List<Trabalhador>();
                cruzamento.AddRange(metadePai);
                cruzamento.AddRange(metadeMae);

                // Exibir os indivíduos em uma tabela
                Console.WriteLine($"[ {i + 1} ] Cruzamento realizado: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Nome\t\tTarefa\tNota");
                Console.ResetColor();

                foreach (var t in cruzamento)
                {
                    Console.WriteLine($"{t.Nome}\t\t{t.Tarefa}\t{t.Nota}");
                }
            }
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
    }
}
