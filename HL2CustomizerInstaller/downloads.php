<!DOCTYPE HTML>
<html>
<head>
	<meta charset="UTF-8">
	<title>HL2 Customizer</title>
	<link rel="stylesheet" href="css/style.css" type="text/css">
</head>
<body>
	<div id="background">
		<div id="header">
			<div>
				<div>
					<a href="index.php" class="logo"><img src="images/logo.png" alt=""></a>
					<ul>
						<li>
							<a href="index.php" id="menu1">home</a>
						</li>
						<li>
							<a href="tutorials.php" id="menu2">tutorials</a>
						</li>
						<li class="selected">
							<a href="downloads.php" id="menu3">downloads</a>
						</li>
						<li>
							<a href="not_available.php" id="menu4">forum</a>
						</li>
						<li>
							<a href="contact.php" id="menu5">contacts</a>
						</li>
					</ul>
				</div>
			</div>
		</div>
		<div id="body">
			<div>
				<div>
					<div class="games">
						<div class="content">
							<h3>Available versions</h3>
							
							<ul>
								<img src="images/versions-alpha.png" width="100" height="30" alt="VERSIONS ALPHA">
								<li>
									<span><a href="download_counter.php?version=0.3">HL2Customizer ALPHA 0.3.0</a></span>
								</li>
								<li>
									<span><a href="download_counter.php?version=0.2.1">HL2Customizer ALPHA 0.2.1</a></span>
								</li>
								<li>
									<span><a href="download_counter.php?version=0.2">HL2Customizer ALPHA 0.2</a></span>
								</li>
								<li>
									<span><a href="download_counter.php?version=0.1">HL2Customizer ALPHA 0.1</a></span>
								</li>
							</ul>
							<ul>
								<li>
								<?php

									$hit_count = @file_get_contents('numberOfdownload.txt');
									echo $hit_count;
								
								?> people have already downloaded HL2Customizer!
								</li>
							</ul>
						</div>
						<div class="article">
							<p>
							<br />
								The last version of HL2Customizer is the version <a href="download_counter.php?version=0.3">Alpha [0.3.0]</a>. You can still download
								older versions but it's not advised. The last version contain more features and less bug.								Warning : the software is still in alpha version, that's mean it may contain bugs and do not
								have all features yet.
								
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div id="footer">
			<div>
				<ul>
					<li id="facebook">
						<a href="http://www.turanic.com">facebook</a>
					</li>
					<li id="twitter">
						<a href="http://store.steampowered.com/">twitter</a>
					</li>
					<li id="googleplus">
						<a href="http://store.steampowered.com/app/320/">googleplus</a>
					</li>
				</ul>
				<p>
					Website made by Turanic
				</p>
				<p>
					@ copyright 2014. all rights reserved.
				</p>
			</div>
		</div>
	</div>
</body>
</html>