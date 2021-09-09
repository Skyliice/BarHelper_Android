# BarHelper_Android
## Small dictionary for drinks and cocktails, add your own recipes and search through them with ease!
## This is the android version of BarHelper. To see the IOS version click <a href="https://github.com/vladukhaDog/BarHelper">here</a>.
## API


 #### POST  adddrink.php  добавляет коктейль в базу
| param | desc |
|--|--|
|name| название коктейля string|
|desc| описание, доп инструкции string|
|recipe| рецепт коктейля "ComponentID:AmountInMl;ComponentID:AmountInMl;" (example 37:50;12:15;) |
|img| url на изображение коктейля |


 #### POST  addliq.php  добавляет компонень в базу
| param | desc |
|--|--|
|name| название компонента string|

#### GET showdrink.php показать все коктейли

#### GET showliq.php показать все компоненты



## App
#### Simple List of drinks with name/description search, touch the drink to see the recipe:

List | Details
------------ | -------------
![GitHub Logo](https://github.com/Skyliice/BarHelper_Android/blob/master/Images/Drinktionary.jpg) | ![GitHub Logo](https://github.com/Skyliice/BarHelper_Android/blob/master/Images/DetailView.jpg)

#### Advanced search, look cocktail with specific components in them or choose all your components at hand and the app will show you what you can do with them, additionaly you can choose up to how many components in a cocktail you are searching.
<p float="left">
  <img src="https://github.com/Skyliice/BarHelper_Android/blob/master/Images/SearchComponent.jpg" width="300" />
  <img src="https://github.com/Skyliice/BarHelper_Android/blob/master/Images/SearchDrink.jpg" width="300" /> 
</p>


### **Add you own recipe to the data base.**
#### Choose a name and description
#### Upload an image from gallery or write down url of an image if its already somewhere online

<p float="left">
  <img src="https://github.com/Skyliice/BarHelper_Android/blob/master/Images/CreateDrink.jpg" width="300" />
</p>

#### Write down the recipe with exact amount of each component

<p float="left">
  <img src="https://github.com/Skyliice/BarHelper_Android/blob/master/Images/CreateDrinkComponents.jpg" width="300" />
</p>


#### Check all the information and upload it to the database!

<p float="left">
  <img src="https://github.com/Skyliice/BarHelper_Android/blob/master/Images/CreateDrinkFinish.jpg" width="300" />
</p>


#### And dont forget that you can add components to the list with ease.

<p float="left">
  <img src="https://github.com/Skyliice/BarHelper_Android/blob/master/Images/CreateLiq.jpg" width="300" />
</p>


## Everything is stored in the MySQL database and hosted online, so all the drinks are available from any device
