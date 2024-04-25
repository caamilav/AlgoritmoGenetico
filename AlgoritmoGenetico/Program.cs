namespace AlgoritmoGenetico
{
    internal class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Alocação de tarefas");

            List<List<Trabalhador>> populacaoFinal = new List<List<Trabalhador>>();

            //Cromosso
            var nomesTrabalhadores = new List<string>
            {
                "Steve", "Robert", "Susan", "Greg", "Austin", "Joe", "Frank", "Abu", "Kelly", "Michael"
            };
            var tarefas = AtribuirTarefas();

            for (int i = 0; i < 10; i++)
            {
                //Inicializar população
                var populacao = InicializarPopulacao(nomesTrabalhadores, tarefas);
                populacaoFinal.Add(populacao);

                Console.WriteLine($"Indíviduo {i + 1}");
                foreach (var trabalhador in populacao)
                {
                    Console.WriteLine($"Nome: {trabalhador.Nome}, Tarefa: {trabalhador.Tarefa}, Nota: {trabalhador.Nota}");
                }

                //Avaliar população
                var resultadoAvaliacao = AvaliarPopulacao(populacao);

                Console.WriteLine($"Resultado da avaliação do indivíduo {i + 1}: {resultadoAvaliacao}");

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
