using System;

namespace Pedreira
{
    class Program
    {
        static void Main(string[] args)
        {
            Pedra[] blocos = new Pedra[10];
            string opcao = null, desc, tipoMaterial, origem, origemPesquisar;
            int id, numBloco, indice = 0, idPesquisar;
            double medidas, valorCompra, valorVenda;
            string nomeArquivo = "blocos.txt";
            string caminhoAreaDeTrabalho = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string caminhoCompleto = Path.Combine(caminhoAreaDeTrabalho, nomeArquivo);

            if(!File.Exists(caminhoCompleto)){
                try{
                    File.Create(caminhoCompleto);
                }catch (Exception ex){
                    Console.WriteLine("Ocorreu um erro ao tentar criar o arquivo para salvar os blocos. Tente novamente!  " + ex.Message);
                }
            }

            while (!(Convert.ToDouble(opcao)==6))
            {
                do
                {
                    Console.WriteLine("[1] - Cadastrar bloco\n[2] - Listar blocos\n[3] - Buscar bloco por ID\n[4] - Listar blocos por pedreira\n[5] - Arquivo Txt\n[6] - Sair");
                    opcao = Console.ReadLine();
                }while (!isNumberInteger(opcao));

                switch (Convert.ToDouble(opcao)) {
                    case 1:
                        Console.WriteLine("ID: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Número do bloco: ");
                        numBloco = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Medidas (em m3): ");
                        medidas = Convert.ToDouble(Console.ReadLine()); 
                        Console.WriteLine("Descrição: ");
                        desc = Console.ReadLine();
                        Console.WriteLine("Tipo do material: ");
                        tipoMaterial = Console.ReadLine();
                        Console.WriteLine("Valor de compra: ");
                        valorCompra = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Valor de venda: ");
                        valorVenda = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Pedreira de origem: ");
                        origem = Console.ReadLine();
                        Pedra produto = new Pedra(id, numBloco, medidas, desc, tipoMaterial, valorCompra, valorVenda, origem);
                        blocos[indice] = produto;
                        try
                        {
                            StreamWriter sw = File.AppendText(caminhoCompleto);
                            sw.WriteLine($"{indice} - {id}, {numBloco}, {medidas}, {desc}, {tipoMaterial}, {valorCompra}, {valorVenda}, {origem}");
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao tentar escrever no arquivo de texto: " + ex.Message);
                        }
                        indice++;
                        break;

                    case 2:
                        for(int i = 0; i < blocos.Length; i++)
                        {
                            verificarVazio(blocos[i]);
                            Console.WriteLine("-----------------------------------");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Digite o ID do bloco: ");
                        idPesquisar = Convert.ToInt32(Console.ReadLine());
                        for(int i = 0; i < blocos.Length ; i++)
                        {
                            if (blocos[i] != null && blocos[i].id == idPesquisar)
                            {
                                verificarVazio(blocos[i]);
                            }
                        }
                        break;
                    case 4: 
                        Console.WriteLine("Digite a pedreira de origem do bloco: ");
                        origemPesquisar = Console.ReadLine();
                        for(int i = 0; i < blocos.Length ; i++)
                        {
                            if (blocos[i] != null && blocos[i].origem == origemPesquisar)
                            {
                                verificarVazio(blocos[i]);
                            }
                        }
                        break;
                    case 5:
                        try{
                            StreamReader sr = new StreamReader(caminhoCompleto);
                            Console.WriteLine(sr.ReadToEnd());
                            sr.Close();
                        }catch (Exception ex){
                            Console.WriteLine("Erro ao tentar ler o arquivo de texto: " + ex.Message);
                        }
                        break;
                }
           
            }
        }

        static bool isNumberInteger(string num) // FUNÇÃO QUE VERIFICA SE UM NÚMERO É INTEIRO OU NÃO
        {
            if (double.TryParse(num, out double numero))
            {
                if (numero % 1 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static void verificarVazio(Pedra bloco) // FUNÇÃO QUE VERIFICA SE UMA POSIÇÃO DA ARRAY TEM OU NÃO OU ELEMENTO DO TIPO "Pedra"
        {
            if(bloco != null)
            {
                mostrarBloco(bloco);
            }
            else
            {
                Console.WriteLine("Vazio");
            }
        }

        static void mostrarBloco(Pedra bloco) // FUNÇÃO QUE MOSTRA O BLOCO NO CONSOLE
        {
            Console.WriteLine("ID: " + bloco.id);
            Console.WriteLine("Número do bloco: " + bloco.numBloco);
            Console.WriteLine("Medidas (em m3): " + bloco.medidas);
            Console.WriteLine("Descrição: " + bloco.desc);
            Console.WriteLine("Tipo do material: " + bloco.tipoMaterial);
            Console.WriteLine("Valor de compra: " + bloco.valorCompra);
            Console.WriteLine("Valor de venda: " + bloco.valorVenda);
            Console.WriteLine("Pedreira de origem: " + bloco.origem);
        }

        public class Pedra // CLASSE DA PEDRA 
        {
            public int id { get; set; }
            public int numBloco { get; set; }
            public double medidas { get; set; }
            public string desc { get; set; }
            public string tipoMaterial { get; set; }
            public double valorCompra { get; set; }
            public double valorVenda { get; set; }
            public string origem { get; set; }

            public Pedra( // MÉTODO CONSTRUTOR DA CLASSE "Pedra"
            int id, 
            int numBloco, 
            double medidas, 
            string desc, 
            string tipoMaterial, 
            double valorCompra, 
            double valorVenda, 
            string origem)
            {
                this.id = id;
                this.numBloco = numBloco;
                this.medidas = medidas;
                this.desc = desc;
                this.tipoMaterial = tipoMaterial;
                this.valorCompra = valorCompra;
                this.valorVenda = valorVenda;
                this.origem = origem;
            }
        }
    }
}