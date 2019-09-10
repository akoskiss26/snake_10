# snake_10
Netacademia snake után önállóan
## megcsináltuk a solution-t

## View és Model elkészítése

- mit csinál a View (ez a MainWindow.xaml)
 - megjeleníti a kígyót
 - megjeleníti a játékszabályokat
 - elkapja a bilentyű leütéseket és továbbítja Model-be

- mit csinál a Model
 - átveszi a billentyűparancsokat
 - tartalmazza a játéklogikát

- a Snake_10 project-en belül csinálunk egy Model foldert
- a Model folderben csinálunk egy Arena osztályt

- az xaml-ben feliratkozunk a KeyDown eseményre, a Window_KeyDown fgv-t fogja meghivni
- ez a fgv az xaml.cs-ben van definiálva, és meghívja az Arena-ban levő KeyDown(e.Key) fgv-t
ami valójában figyeli hogy leütötték-e a nyíl billentyűt

### Elkészítjük a játékmezőt
- az xaml-ben megcsináljuk a 20x20 kockából álló mezőt a 450x450 területű MainWindow-ban 

- nugets-el telepítjük a fontawesome-ot
 - MainWindow-ban megadjuk a névteret: xmlns:fa="http..."  (megadja)
 - minden kockába teszünk ikont:	
   -az Arena gridbe: <fa:ImageAwesome Icon = "SquareOutline" Grid.Column="0" Grid.Row="0"
   és így tovább


## a kígyó fejének megjelenítése:
 [] AZ ArenaGrid.Children[] gyűjtemény egy tagját kivesszük, és az általános UIElement típusból átalakítjuk
	(majdnem konvertáljuk) FontAwesome.WPF.ImageAwesome osztályúvá (típusuvá)
 [] ennek az típusváltoztatott elemnek már van Icon tulajdonsága, és ezt változtatjuk pl. circle-re a SquareOutline-ról
