E-commerce de Fast Food (ASP.NET Web Forms) - Crazy Pizza
Alunos: Eduardo José Rodrigues e João Santolin Azevedo.

-- LOGINS PARA TESTES --
Administrador:
Usuário: admin
Senha: admin

Usuário Comum:
Usuário: usuario
Senha: usuario

Projeto acadêmico de desenvolvimento de uma aplicação em ASP.NET Web Forms, focado em criar uma interface moderna, completa, 
responsiva e funcional para pedidos de comida, neste caso, uma pizzaria. O objetivo é simular um sistema completo com cardápio online, onde o 
usuário visualiza, se cadastra e intrage com os produtos e podendo adicioná-los ao carrinho, promovendo uma experiencia completa.

- Tecnologias utiizadas:

	Estrutura de páginas (back-end): C# ASP.NET WebForms
	Estrutura de páginas (front-end) - 
		Estrutura e estilização: HTML, CSS e Bootstrap
		Interatividade: JavaScript
	Gemini Pro: Geração de imagens por IA.
	Google Fonts: ícones botões.
	Banco de dados - SQL Server


- Estrutura de Pastas:
	- Painel do Administrador
		Default.aspx
		FrmCaroussel.aspx
		FrmPedidos.aspx
		FrmPedidosDetalhes.aspx
		FrmProdutos.aspx
		FrmUsuario.aspx
		AdminPage.cs
		CarousselDAO.cs
		CategoriaDAO.cs
		Usuario.DAO.cs
		Web.Config
		Admin.CSS

	- Painel do Usuário
		Default.aspx 
		FrmCadastro.aspx
		FrmCarrinho.aspx
		FrmCheckout.aspx
		FrmLogin.aspx
		FrmPedidos.aspx
		PedidoDAO.cs
		ProdutoDAO.cs
		Sha1Hasher.cs
		Web-Config
		Style.CSS

	O projeto basicamente seguiu duas frentes, o painel do administrador, onde de fato são gerenciados e controlados as funcionalidades que o 
	usuário irá acessar, no painel do admninistrador é possível cadastrar, editar e remover usuários e produtos, além de controlar o caroussel
	que está presente na homepage do usuário. O administrador também possui controle sobre os pedidos que foram feitos pelo usuário podendo 
	alterar o status do mesmo prontamente. Para melhor manuseio da estilização, todas as telas referentes ao administrador foram submetidas 
	a um arquivo .CSS separado da telas do usuário. 

	Já no painel do usuário, onde de fato o "cliente" acessa, é possível se cadastrar, acessa o cardápio diretamente na homepage onde interage 
	com os mesmo, podendo filtrar os produtos de acordo com sua preferencia ou adicionando-os diretamente no "Carrinho", na tela do carrinho o 
	usuário pode verificar os itens que escolheu e prosseguir com o pedido. Toda a estilização das páginas direcionadas ao usuário estão presentes
	no arquivo .CSS destinado a ele.
	
- Banco de dados
O banco de dados foi desenvolvido usando Entity Framework com SQL Server e permitirá transformar ferramentas estáticas em produtos dinâmicos, 
possibilitando inserir, editar e excluir itens via sistema administrativo no futuro.


- Decisões de Design
Para o design de nosso e-commerce foi adotado um layout limpo, sóbrio e condizente com a proposta, fundo de cor quente mas em tom claro, com 
a Navbar e footer em tons mais fortes, sempre piorizando a resposividade e fluidez na navegação. 
No painel do Administrador foi proposto um tema de cor mais fria mas sem perder a identidade visual que propusemos. 
