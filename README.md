# ProjetoLabSoftware

Projeto desenvolvido para o Grupo 4 da disciplina de laboratório de software.
Contato para dúvidas

Requisitos Backend \
.Net Core 5.0 \
Visual Studio 2019

Passos para rodar a aplicação Backend \
Passo 1: Baixar os repositórios do GitHUb. \
Passo 2: Configurar a connection string no caminho ProjetoLabSoftware/Api/Api.Data/Context/ContextFactory.cs \
Passo 3: Configurar a connection string no caminho ProjetoLabSoftware/Api/Api.Aplication/appsettings.json \
Passo 4: Alterar as "TokenConfigurations" de acordo com a necessidade \
Passo 5: Com as conections strings configuradas rodar o comando de update da migrations Ex: dotnet ef database update \
Passo 6: Clicar em start na aplicação do visual studio. 

Requisitos Frontend \
Nodejs LTS \
Angular cli 

Passos para rodar a aplicação Frontend \
Passo 1: Baixar os repositórios do GitHUb. \
Passo 2: Configurar a url da api "this.setUrlApi" no caminho ProjetoLabSoftware/Front/src/app/providers/global.service.ts \
Passo 3: Configurar os tokens "GoogleLoginProvider" e "FacebookLoginProvider" no caminho ProjetoLabSoftware/Front/src/app/app.module.ts \
Passo 4: Rodar o comando "npm i" para instalar todas as dependências do angular. \
Passo 4: Rodar o comando "ng serve -open" para rodar a aplicação.
