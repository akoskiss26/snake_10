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
 - [] AZ ArenaGrid.Children[n] gyűjtemény egy tagját kivesszük, és az általános UIElement típusból átalakítjuk
	(majdnem konvertáljuk) FontAwesome.WPF.ImageAwesome osztályúvá (típusuvá)
 - [] ennek az típusváltoztatott elemnek már van Icon tulajdonsága, és ezt változtatjuk pl. circle-re a SquareOutline-ról

 ### megcsináljuk a snake osztályt (a Model kvtárba)
 - a snake osztály tartalmazza a kígyó tulajdonságait:
		ArenaPosition HeadPosition - prop a fej poziciójára 
		SnakeHeadDirectionEnum HeadDirection - enum a fej mozgásának irányára

### megcsináljuk a pendulum-ot
- a pendulum az arena konstruktorában lesz
- minden taktjelére lefut a fgv, ami lépteti a kígyót (ItsTimeToDisplay) 

### a kígyó mozgatása
- az ItsTimeToDisplay fgv-ben switch-el szelektáljuk a megnyomott nyíl gombot, és annak megfelelően számoljuk a HeadPosition-t
- a griden a fej  megjelenítést a ShowSneakHead fgv-be szerveztük, ehhez még gyártottunk
egy icon enum-ot, amivel jelezzük h. milyen ikont akarunk megjeleníteni a fgv-el
- eltüntetjük a régi fejet, helyette test ikon lesz

### a kígyó test megjelenítése
 - a test adatait egy listában tároljuk, melynek elemei ArenaPosition típusuak: List<ArenaPosition> Body
	itt vigyázunk, hogy az object reference enull hiba elkerülésére példányosítani kell egy ilyet az osztály konstruktorban 
 - ha hatnál nagyobb lenne a kígyó, törölnünk kell a végét
 
### ütközések kezelése
 - a kígyó feje a pályán kívülre kerül: figyeljük a fej row, column poziciót,
   ha meghaladja a határértékeket (0, 19) akkor vége a játéknak
 - a kígyó saját magát keresztezi: a fej pozicióját összevetjük a Body-ban tárolt poziciókkal, 
   ha egyezés van, vége a játéknak
   Ez megcsinálható foreach ciklussal, vagy egyszerűbben Linq alapon: Snake.Body.Any(x=> x.rowPosition == Snake.HeadPosition. RowPosition && ....) 

### elemózsia elhelyezése 
 - a játéktéren véletlenszerűen elhelyezünk elemózsiát ennek helyét listában tároljuk (ha később több elemózsia lenne egyidőben a játéktéren)
 -- készítünk egy véletlenszám generátort
 -- megcsináljuk a GetFood() fgv-t az Arena-ban, ez generál egy ArenaPositon-t, megnézi hogy ez a pozició foglalt-e, ha igen: generál újat, ha nem: 
    ezt a poziciót beírja a Foods listába (mint elemózsia pozicióját), és megjelenít ezen a pozición egy piros almát. A fgv-t először a StartOfGame() fgv-ből hívjuk

### elemózsia megevése
- figyeljük hogy a kígyófej poziciója egybeesik-e a foods pozicióval. Ha igen, akkor:
	* eltüntetjük az elemózsiát a játéktérről (a pozicióban átíródik fejre, nem kell csinálni semmit)
	* töröljük az elemózsiát a FoodPositions listáról
	* új elemózsiát generálunk
	* megnyújtjuk eggyel a kígyót
	* gyorsítjuk a kígyót
- megvalósítás:
	* ItsTimeToDisplay()-ben diszkutáljuk hogy a fej ütközött-e elemózsiával
	* Ha igen, meghívjuk az Eating() fgv-t, ebben:
		* FoodPositions[0] töröljük
		* GetFood() fgv-el újat generálunk és megjelenítjük
		* kígyó hosszát megnöveljük Lenght = Lenght+1
	* a pendulum példányosítást kivesszük külön fgv-be (PendulumStart)
	* A PendulumStart(x) fgv paramétere a megevett ételek száma, ebből számolja a fgv a taktjel sebességét
	* A PendulumStart(x) fgv-t meghívjuk a StartOfGame()-ben és az Eating(x,y) fgv-ben 


# A működés:
 Window_KeyDown [figyeli a billentyűleütést], hívja:
 arena.KeyDown(e) 
   - ha még nem indult el a játék: hívja StartOfGame() -t
   - ha már elindult beállítja a Snake.HeadDirection -t

 StartOfGame() 
   - hívja a PendulumStart(int numberOfEaten) -t
   - hívja a GetFood() -t

 PendulumStart(int numberOfEaten)
   - ha megy, akkor leállítja a pendulum-ot
   - újraindítja az új frekvenciával

 GetFood()
   - generál egy véletlen helyet
   - a Grid-en ide feltesz egy food-ot:
     - meghívja: ShowSnakeHead(randomRow, randomColumn, IconEnum.Food);
   - a Canvas-on is feltesz egy food-ot:
     - meghívja a PaintOnCanvas(row, col) fgvt, amely a paint-tet belerakja a View.ArenaCanvas.Children gyűjteménybe, és a paint-tel tér vissza, 
	 - meghívja a Foods.Add(row, col, paint) -et, hogy eltároljuk a food-ot




 ItsTimeToDisplay(object sender, EventArgs e)
   - megjegyzi a fej pozicióját
   - a SnakeHeadDirection alapján új fejpoziciót számol
   - Snake.HeadPosition aalapján falnak ütközés detektálása (ha igen -> EndOfGame())
   - Snake.Body alapján testtel ütközés detektálása (ha igen -> EndOfGame)
   - Griden új fejet megrajzolja
   - régi fejpoziciót átrakja a Body listába
   - régi fej helyébe testet rajzol
   - ha megvan a test engedélyezett hossza, törli a Body[0] elemet, a test végét
   - vizsgálja van-e evés
     - ha van evés, meghívja: Eating(food.RowPosition, food.ColumnPosition);
   

 Eating(int rowPosition, int columnPosition)
   - a törlendő elem poziciója alapján a Food.Remove() a törlendő elemmel tér vissza
   - az EraseFromCanvas a törlendő elem .Paint tulajdonsága alapján törli azt a Canvas-ról
   - az étellistáról (FoodPositions) töröljük a 0. elemet
   - kérünk új ételt (GetFood())
   - növeljük a kígyó hosszát
   - növeljük a megevett ételek számát
   - újraindítjuk a pendulum-ot (PendulumStart())