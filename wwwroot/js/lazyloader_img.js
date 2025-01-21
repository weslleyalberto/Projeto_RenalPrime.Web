document.addEventListener("DOMContentLoaded", () => {
    const placeholders = document.querySelectorAll(".placeholder");

    const lazyLoad = (entries, observer) => {
        entries.forEach((entry) => {
            if (entry.isIntersecting) {
                const placeholder = entry.target;
                const imageUrl = placeholder.getAttribute("data-src");

                // Simula o carregamento com atraso de 3ms
                setTimeout(() => {
                    // Cria a imagem real
                    const img = document.createElement("img");
                    img.src = imageUrl;
                    img.alt = "Imagem carregada via Lazy Load";

                    // Substitui o esqueleto pela imagem após o carregamento
                    img.onload = () => placeholder.replaceWith(img);

                    observer.unobserve(placeholder); // Para de observar o elemento
                }, 3000); // Define o atraso em milissegundos
            }
        });
    };

    const observer = new IntersectionObserver(lazyLoad, {
        root: null,
        rootMargin: "0px 0px 50px 0px",
        threshold: 0.1,
    });

    placeholders.forEach((placeholder) => observer.observe(placeholder));
});