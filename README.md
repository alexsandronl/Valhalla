![alt text](https://github.com/alexsandronl/Valhalla/blob/main/logo.jpg?raw=true)

Hackathon TIT/Hypercloud

Time:
- Alexsandro Nunes Lacerda
- Carina Cláudio Oliveira
- Rayssa Araújo de Oliveira

### Problema a ser solucionado:

- Com mundo globalizado hoje em dia, e com muitas empresas atuando com colaboradores remotos, quase sempre as pessoas fora do seu ciclo diário de trabalho, não se conhecem, ou até mesmo sabem quem são as pessoas, ou qual atuação delas dentro da empresa.

### Solução do problema:

- O Valhalla veio para ajudar nesta tarefa, atravéz de uma ferramenta gameficada que ajuda os colaboradores conhecerem os outros colaboradores da empresa de uma forma divertida, através de um jogo de cartas, estilo RPG, aonde cada colaborador tem as suas cartas, com seu nome, foto, informações do seu cargo e setor, bem como o seus pontos dentro da empresa baseado na sua hierarquia, nível de conhecimento e especializações.
- E como um benefício adicional, o jogo motiva as pessoas a se especializarem para que o administrador da ferramenta, que muitas das vezes seria o papel do RH, podem editar a carta do colaborador e aumentar a sua pontuação baseado em pré-requisitos, como especializações, certificados, etc..

### Objetivo do Jogo:

- O jogo se baseia no objetivo de vencer cada rodada, mas sempre pensando em não gastar muitos pontos das carta, ou seja, utilizar o número de pontos suficiente para vencer cada rodada, para economizar cartas para as próximas rodadas.
- Cada rodada é composta por turnos e em cada turno, o jogador poderá escolher dentro de um tempo, se ele coloca uma carta na mesa, ou se passa a vez.
- No final ganha o jogador que conseguiu vencer o maior número de rodadas.

### Regras do Jogo:

- Assim que o jogador se sentir pronto, ele clica em 'Pronto', localizado abaixo do seu nome.
- Cada jogador começa com um número especifico de cartas no deck, sorteadas aleatoriamente.
- No primeiro turno, da primeira rodada, é obrigatório que o jogador comece jogando uma carta.
- Para jogar uma carta, o jogador clica na carta, olha se a carta atende, e clica em 'Jogar a carta', sempre na sua vez de jogar.
- Escolhendo uma carta, e colocando a carta na mesa, a soma dos seus pontos daquela rodada é mostrada do lado direito das suas cartas.
- As cartas com o número de pontos na cor preta, significa que não tem pontos extras, e as cartas que possuem pontos extras, fica com os pontos na cor azul.
- Em um turno, caso o jogador ache que os pontos que ele já colocou ate então naquela rodada, seja suficiente, o jogador poderá passar a vez.
- Caso o jogador não tenha cartas na mesa, ele terá que obrigatóriamente passar a vez.
- A cada passada de vez, o jogador ganha uma nova carta no seu deck para ser usada no próximo turno.
- Assim acontece sucessivamente a cada rodada e a cada turno.
- No final ganha o jogador que conseguiu vencer o maior número de rodadas.
- E a pontuação final do jogador vencedor é calculado pela soma de todos os pontos das cartas jogadas na mesa em todas as rodadas, somado de 50 pontos pela vitória, e ainda acrescido de um bônus por cada carta que sobrou em seu deck.
- A pontuação final do jogador naquele jogo, é somado ao seu ranking, que é mostrado na página inicial.

### Características técnicas da aplicação:

- Aplicação desenvolvido a tecnologia Microsoft Blazor Server utilizando C# na versão 7.0.
- Iniciamente a aplicação usa como base de dados, arquivos json para cadastrar as cartas, jogadores, e resultado dos jogos, mas futuramente será migrado para um banco de dados gerenciavel, seja relacional ou no-sql.
- A autenticação dos jogadores é feita utilizando o OAUTH2 do LinkenIn.
- As configurações do jogo como, número de cartas iniciais, número de rodadas, número de turnos e tempo por turno, bem como a apiKey e secret do LinkedIn, e senha para acesso ao cadastro das carta, são parametrizaveis no arquivo appsettings.json que fica na raiz da aplicação gerada.
- Os arquivos json, onde ficam a base de dados, são gerados automaticamente em uma pasta dentro da aplicação com o nome de 'database'.
- Nenhum dado sensível do jogador são armazenados, sendo principalmente o nome e o e-mail, para identificação do jogador na aplicação.
- A aplicação conta com configurações de Docker/Docker Compose.

**A aplicação está divida nos seguintes projetos:**
* Valhalla.App (Aplicação Blazor Server)
* Valhalha.Domain (Bliblioteca de domínio)
* Valhalla.Infraestrutura (Biblioteca de serviços)
* Valhalla.UnitTest (Testes unitários)
* BlazorInputFile (Componente de upload de arquivos)

**As dependencias utilizadas são:**
![alt text](https://github.com/alexsandronl/Valhalla/blob/main/Diagrama.png?raw=true)
* Blazored.Toast 4.0.0
* System.Configuration.ConfigurationManager 7.0.0
* Autofac 6.5.0
* Autofac.Extensions.DependencyInjection 8.0.0
* HtmlAgilityPack.NetCore 1.5.0.1
* Blazorise 1.2.0
* Blazorise.Icons.FontAwesome 1.2.0
* Blazorise.Bootstrap 1.2.0
* Blazorise.Components 1.2.0
* Blazorise.DataGrid 1.2.0
* System.Drawing.Common 7.0.0

**Parametrizações no arquivo appsettings.json:**
* "LinkedIn": {
    "clientId": "{Cliente ID do LinkedIn }",
    "clientSecret": "{Secret do LinkedIn }"
    
* "Tabuleiro": {
    "NumeroCartasIniciais": {Cartas Iniciais},
    "NumeroDeRodadasPorJogo": {Número de Rodadas por Jogo},
    "NumeroDeTurnosTotaisPorRada": { Número de Turnos por Rodada},
    "TempoPorTurno": "{Tempo por Turno no formato: hh:mm:ss"
    
* "Administracao": {
    "SenhaDeAcesso": "{Senha de acesso a Administração}"

### Instalação da Aplicação:

- Para instalar a aplicação, o usuário precisará baixar o jogo e compilar, gerando um docker, ou publicando os arquivos.
- No local onde a aplicação for publicada, deverá ter instalado o .Net 7, e sendo um servidor IIS, utiliza a verão Bundle.
- Para se cadastrar as cartas, o usuário deverá acessar o endereço: "http(s)://{dominio}/CadastroDeCartas" e entrar com a senha de administração definida nas parametrizações da aplicação.
- Para acessar os jogadores cadastrados, o usuário deverá acessar o endereço: "http(s)://{dominio}/UsuariosCadastrados" e entrar com a senha de administração definida nas parametrizações da aplicação.
- Crie um app no console de developer do LinkedIn, para gerar o clientID e secretKey.
- Adicione a url de callback com o dominio do seu site: http(s)://{dominio}/signin-linkedin

### Melhorias futuras a serem implementadas:
  
  - Troca da base de dados para um banco gerenciável.
  - Implementação para upload de avatares.
  - Implementação de rotina para converter as imagens feitas upload (cartas e avatares) para que seja armazenados com tamanho pequeno mas qualidade aceitável.
  - Melhoria no UI e UX.
  - Melhoria na IA para jogar contra o computador.
  - Implementação do modo Multiplayer (a aplicação já possui diversos códigos já implementados para essa finalidade).
  - Escrever testes unitários.
  - Refatoração de códigos para uma melhor arquitetura.
  - Implementação de novos atributos especiais para as cartas, onde uma carta poderá anular pontos de outras baseado no cargo, setor ou outra característica.
  - Implementação de cenários de jogos, onde dependendo do cenário, algumas cartas podem ter mais ou menos vantagens.
  - etc...
