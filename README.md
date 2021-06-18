# Happy Bank :)

Um banco digital, sem burocracia e fácil de usar!!

## Alunos integrantes da equipe

* Breno Moreira Proti de Castro - moreirasgbr@gmail.com
* Gabrielle Silva de Paula - ggabriellesilva4@gmail.com
* Gustavo Augusto Caldeira dos Santos - caldeira.santos@gmail.com
* Hugo José Ferreira Moreira - hugojose39@yahoo.com
* Maicon Douglas Marcelino - maicondouglasm19@gmail.com

## Professores responsáveis

* Marco Paulo Soares Gomes
* Joyce Christina de Paiva Carvalho

## Instruções de utilização

Para execução do sistema você deverá executar os seguintes passos:

1. Criação do banco de dados
1. Execução do banckend
1. Execução do frontend

### Execução do backend

#### Criação do banco de dados
Para execução do backend é requerido PostgreSQL. O arquivo de criação do banco de dados está na pasta Codigo

1. Crie um banco de dados para aplicação (Ex: happybank)

1. Execute o comando `psql -d happybank -a -f happybank.sql`

#### Execução da API

Na pasta **/Codigo/HappyBank** execute os comandos

    dotnet restore
    dotnet run

#### Execução do Frontend

Na pasta **/Codigo/HappyBank/Front-end/happy-bank** execute os comandos

    npp install
    npm start
