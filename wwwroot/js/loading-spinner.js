
document.addEventListener("DOMContentLoaded", function () {
    const mainElement = document.querySelector('main'); // Seleciona o elemento <main>
    const spinner = document.createElement('div'); // Cria o spinner
    spinner.id = 'spinner'; // Define o id para o spinner
    spinner.style.display = 'none'; // Inicialmente escondido
    document.body.appendChild(spinner); // Adiciona o spinner ao body

    // Define o estilo básico para o spinner
    spinner.style.position = 'fixed';
    spinner.style.top = '50%';
    spinner.style.left = '50%';
    spinner.style.width = '50px';
    spinner.style.height = '50px';
    spinner.style.border = '5px solid var(--sauvignon)';
    spinner.style.borderRadius = '50%';
    spinner.style.borderTopColor = 'var(--verde-escuro)';
    spinner.style.animation = 'spin 1s linear infinite';
    spinner.style.transform = 'translate(-50%, -50%)';
    spinner.style.zIndex = '9999'; // Garantir que fique sobre o conteúdo

    // Animação de rotação do spinner
    const style = document.createElement('style');
    style.innerHTML = `
        @@keyframes spin {
            0 % { transform: rotate(0deg); }
                        100% {transform: rotate(360deg); }
                    }
        `;
    document.head.appendChild(style);

    // Remove o fade-out quando a página carrega (fade-in)
    mainElement.classList.remove('fade-out-main');

    // Adiciona o evento de clique em todos os links
    const links = document.querySelectorAll('a');
    links.forEach(link => {
        link.addEventListener('click', function (event) {
            const target = this.href;
            const isSamePageLink = target.includes("#") && (target.split("#")[0] === window.location.href.split("#")[0]);

            // Se for um link para a mesma página (âncora), não faz o efeito
            if (!isSamePageLink) {
                event.preventDefault(); // Evita o carregamento imediato da página

                // Adiciona a classe para iniciar o fade-out no main
                mainElement.classList.add('fade-out-main');

                // Mostra o spinner
                spinner.style.display = 'block';

                // Aguarda o tempo da animação antes de redirecionar
                setTimeout(function () {
                    window.location.href = target;
                }, 1000); // O tempo aqui deve corresponder à duração do 'transition' no CSS
            }
        });
    });
});
