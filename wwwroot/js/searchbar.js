const input = document.getElementById("group-input");

$(document).ready(function () {
    $('#keyword').on('input', function () {
        var query = $(this).val();

        // Verifica se o usuário digitou pelo menos 4 caracteres
        if (query.length >= 4) {
            // Faz a chamada AJAX para o backend
            $.ajax({
                url: 'http:/Projeto_RenalPrime.Web_1/WebScraping/Search', // URL da rota de busca no ASP.NET
                method: 'GET',
                data: { searchTerm: query }, // Termo de busca passado ao backend
                success: function (data) {

                    // Limpa os resultados anteriores
                    $('#search-results').empty();
                    console.log(data.length)
                    if (data.length > 0) {
                        input.style.borderRadius = "20px 20px  0 0";
                        // Popula os novos resultados
                        $.each(data, function (index, item) {
                            $('#search-results').append(
                                `<div class="result-item">
                                                                        <a href="${item.url}">${item.title}</a>
                                                                                <p>${item.content}</p>
                                                                    </div>`
                            );
                        });
                    }
                    else {
                        input.style.borderRadius = "100px";

                    }

                },

            });
        }

        else {
            // Limpa os resultados se a entrada for menor que 4 caracteres
            input.style.borderRadius = "100px";
            $('#search-results').empty();
        }
    });
});