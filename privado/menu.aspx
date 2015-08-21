<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu.aspx.vb" Inherits="privado_menu" %>

<!doctype html>
<html lang="en">

<head>
	<meta charset="utf-8"/>
	<title> BIO * NATURAL Online</title>
	
	<link rel="stylesheet" href="../css/layout.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="../css/sexyalertbox.css" type="text/css" media="screen" />
	
    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
	<script src="../js/jquery-1.5.2.min.js" type="text/javascript"></script>
	<script src="../js/hideshow.js" type="text/javascript"></script>
	<script src="../js/jquery.tablesorter.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquery.equalHeight.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
	<script type="text/javascript">



	    $(document).ready(function () {

	        //       $.get("include.html", function (data) {
	        //           $('#sidebar').html(data);
	        //       });

	        $(".tablesorter").tablesorter();

	        //When page loads...
	        $(".tab_content").hide(); //Hide all content
	        $("ul.tabs li:first").addClass("active").show(); //Activate first tab
	        $(".tab_content:first").show(); //Show first tab content

	        //On Click Event
	        $("ul.tabs li").click(function () {

	            $("ul.tabs li").removeClass("active"); //Remove any "active" class
	            $(this).addClass("active"); //Add "active" class to selected tab
	            $(".tab_content").hide(); //Hide all tab content

	            var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
	            $(activeTab).fadeIn(); //Fade in the active ID content
	            return false;
	        });

	    });
    </script>
    <script type="text/javascript">
        $(function () {
            $('.column').equalHeight();
        });
</script>

</head>


<body>

	<header id="header">
		<hgroup>
			<h1 class="site_title"><a href="menu.aspx">BIO * NATURAL Online</a></h1>
			<h2 class="section_title">BIO * NATURAL</h2><div class="btn_view_site"><!--<a href="http://www.medialoot.com">View Site</a>--></div>
		</hgroup>
	</header> <!-- end of header bar -->
	
	<section id="secondary_bar">
		<div class="user">
			<p>Usuario Conectado: <asp:Label Text="" style="text-transform:uppercase;color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;" ID="usuario" runat="server"></asp:Label></p>
			<!-- <a class="logout_user" href="#" title="Logout">Logout</a> -->
		</div>
		<div class="breadcrumbs_container">
			<article class="breadcrumbs"><a href="menu.aspx">BIO * NATURAL Online</a> <div class="breadcrumb_divider"></div> <a class="current">Web</a></article>
		</div>
	</section>
	
	
       <aside id="sidebar" class="column">
       
            <!-- #INCLUDE FILE="include.html" -->

        </aside>
	
	<section id="main" class="column">
		
				
		<article class="module width_full">
			<header><h3>Sistema automatizado web</h3></header>
				<div class="module_content">
					<img src="../images/medicina-natural1.jpg">
				</div>
			</footer>
		</article><!-- end of post new article -->
		
		<!-- end of styles article -->
		<div class="spacer"></div>
	</section>


</body>

</html>
