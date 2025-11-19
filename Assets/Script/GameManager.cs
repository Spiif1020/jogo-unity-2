using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // --- Telas e Painéis ---
    public GameObject telaGameOver;
    public GameObject telaVitoria;
    public GameObject pauseMenuPanel;

    public string mainMenuSceneName = "MainMenu"; // Nome da cena do menu principal

    // --- Estado do Jogo ---
    private bool jogoAtivo = true;
    private bool isPaused = false;

    // --- Cronômetro ---
    private float tempoDecorrido = 0f;

    // --- UI ---
    public TextMeshProUGUI textoTimer;

    void Start()
    {
        // Garante que o jogo inicie ativo e não pausado
        Time.timeScale = 1f;
        isPaused = false;

        // Esconde as telas no início
        if (telaGameOver != null) telaGameOver.SetActive(false);
        if (telaVitoria != null) telaVitoria.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        // Reinicia o cronômetro
        tempoDecorrido = 0f;
        AtualizarTextoTimer();
    }

    void Update()
    {
        // --- Controle de Pausa ---
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }

        // --- Atualiza o cronômetro apenas se o jogo estiver ativo e não pausado ---
        if (jogoAtivo && !isPaused)
        {
            tempoDecorrido += Time.deltaTime;
            AtualizarTextoTimer();
        }
    }

    // --- Funções de Estado do Jogo ---
    public void VencerJogo()
    {
        if (!jogoAtivo) return;
        jogoAtivo = false;
        if (telaVitoria != null) telaVitoria.SetActive(true);
        Debug.Log("VITÓRIA!");
    }

    public void PerderJogo()
    {
        if (!jogoAtivo) return;
        jogoAtivo = false;
        if (telaGameOver != null) telaGameOver.SetActive(true);
        Debug.Log("GAME OVER!");
    }

    // --- Botões ---
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do Jogo...");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    // --- Sistema de Pausa ---
    public void PauseGame()
    {
        if (!jogoAtivo) return;
        isPaused = true;
        Time.timeScale = 0f; // pausa física
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
    }

    // --- Atualiza o texto do cronômetro ---
    private void AtualizarTextoTimer()
    {
        if (textoTimer != null)
        {
            // Mostra em formato mm:ss (ex: 02:35)
            int minutos = Mathf.FloorToInt(tempoDecorrido / 60f);
            int segundos = Mathf.FloorToInt(tempoDecorrido % 60f);
            textoTimer.text = $"{minutos:00}:{segundos:00}";
        }
    }
}