using OpenAI.Chat;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarBanda : Menu
{
    // 1. Criamos um campo para guardar a chave que o Program.cs vai nos dar
    private string _chave;

    // 2. Criamos um "Construtor": ele obriga quem criar este menu a passar a chave
    public MenuRegistrarBanda(string chave)
    {
        _chave = chave;
    }

    public override void Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        base.Executar(bandasRegistradas);
        ExibirTituloDaOpcao("Registro das bandas");
        Console.Write("Digite o nome da banda que deseja registrar: ");

        string nomeDaBanda = Console.ReadLine()!;
        Banda banda = new Banda(nomeDaBanda);
        bandasRegistradas.Add(nomeDaBanda, banda);

        ChatClient client = new ChatClient("gpt-4o-mini", _chave);

        var resposta = client.CompleteChat($"Resuma a banda {nomeDaBanda} em 1 parágrafo. Adote um estilo informal.");

        banda.Resumo = resposta.Value.Content[0].Text;

        Console.WriteLine($"\nA banda {nomeDaBanda} foi registrada com sucesso!");

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
