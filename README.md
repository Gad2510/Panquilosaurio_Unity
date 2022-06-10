# Panquilosaurio Unity


## Abstract
  
  El proyecto por el momento del 21 de marzo del 2022 conocido como Panquilosaurio, tienen como función poder comunicar los hallazgos arqueológicos en cuanto a criaturas que se encuentran entre los periodos Triásico al Cretácico de la historia, a niños de entre 10 a 15 años. A través de un juego que también los ayude a relacionar repostería local de sus países con las creaturas encontradas en el país de origen. 
Introducción

## Antesedentes
  
  Panquilosaurio es un juego el cual se basó en la idea de poder recrear un meme de un anquilosáurido echo en forma de concha, el cual sirvió de inspiración para empezar a crear este mundo, introduciendo con la pregunta, ¿Qué pasaría si pudiera hacer dinosaurios hechos de pan?
  
  Fue entonces que empecé a investigar sobre los hallazgos arqueológicos encontrados en México, peguntándoles a un amigo que trabaja en el INAH (Instituto Nacional de Antropología e Historia), sobre una base de datos de estas creaturas prehistóricas, al leer un poco sobre dichos hallazgos me percate de 2 cosas, en primer lugar, que yo como fanático de los dinosaurios no conozco ni un cuarto de las creaturas encontradas dentro del territorio mexicano.

  Y, en segundo lugar, que también desconozco de la gastronomía de muchos estados de la república. Recordaba los dulces de leche que alguna vez compre, las cachetadas, gastronomía única de los estados en los que eh vivido como son las tlayudas de Oaxaca, la capirotada de Guadalajara, las torrejas de Guerrero y me doy cuenta de que es algo que no solo desconozco de mi país, si no de otros países.

## Preparando la masa

  Para empezar el proyecto se hizo un documento de diseño tratando de resaltar los aspectos básicos del mismo, donde se manejó el core loop design del juego (el flujo base del juego) y el gameflow (la secuencia de escenas que se va a seguir) ambos en grandes rasgos. También se hizo un desglose en vista general de cada uno de los aspectos principales del juego como el manejo de bosses, los dinopostres, personalización, recompensas etc. 
  
  ![Core game loop](/READMEimg/CoreGameLoop.png)
  ![Gameflow](/READMEimg/Gameflow.png)
 
  También fue importante resaltar los aspectos artísticos que e iban a usar para el juego, tomando como referencia un juego llamado Pokémon Rumble, un juego de acción / hack and slash, el cual proporciona una estética simple para la creación de diversas creaturas, tomando eso en cuenta y los aspectos mas resaltantes de la repostería mexicana define las bases del aspecto visual.
  
  Como el proyecto resulto será mas grande de lo que se esperaba, se tuvo que tomar la decisión de reducir las expectativas para los meses de desarrollo. Para lo que se solo se propuso la construcción de una demo funcional con las funciones básicas del juego junto con el desarrollo de 4 creaturas para la jugabilidad con una habilidad como mínimo que sería una básica para presentación. Por lo que dentro de la definición de las creaturas a crear se filtraron solo 4 que representarían una de cada tribu de dinopostres, los frutales, neutros, hojaldre y rellenos, excluyendo la tribu de chocolates y caramelos.
  
  Una vez definido esto fue importante definir una lista de assets así como empezar a trabajar en los modelos dentro del software Maya para un proceso de creación más fluido. En el lapso de un mes se pudo terminar la creación de las 4 creaturas en los aspectos de modelo, skinning y rig para una futura animación y se exporto al motor de maya.
  
 ![Modelos de los dinos](/READMEimg/Modelos.png)
 
  Una vez acabado con eso me dispuse a empezar armar un block mesh de todas las escenas esenciales del juego para poder hacer pruebas a futuro y para que si conseguía el apoyo necesario poder empezar el desarrollo del arte. Cree 3 escenas la principal donde se seleccionan los niveles conocida como CRIADERO, después un área de juego PRADERA DE CRIANZA con dos pisos para pelear con enemigos y uno de boss final, por último, fue el VOLCÁN o la fase de pelea con el boss final de la zona. Todo esto después me permitió desglosar todo los assets que se iban a requerir para la demo, dentro de un archivo de Excel, en conjunto con una carpeta de referencias visuales.
  
  ![Escena credero](/READMEimg/Criadero_Pers.png)
  ![Preview del volcan con el jefe](/READMEimg/Volcan_Image.png)
  ![Preview de la estructura de los stage](/READMEimg/PrederaCrianza_Top.png)

## Empezamos amasar 

  Uno de los grandes retos para empezar el desarrollo de un juego es saber cómo empezar a programar y más pensarlo para poder expandirlo a futuro a una modalidad online multijugador, tomé toda una semana para crear un archivo de la arquitectura del programa con el cual fui desglosando tas las posibles funciones que podía tener el juego. Esto me ayudo de guía inicial, a pesar de estar mal estructurado por la poca experiencia que tenía haciéndolos. Una de las primeras cosas que me quise enfocar fue en el combate del jugador, por lo que antes de combatir primero hay que hacer un jugador. Con los modelos ya hechos empecé a desarrollar mi primer controlador con el modelo base del Agujaceratops (Triceratops). 
  
  ![Imagen de arquitectura del programa](/READMEimg/Panquilosaurio Arquitecture.png)

### El jugador 
  
  Para el movimiento del jugador se empleo el sistema de físicas de unity haciendo que su movimiento dependiera de dos factores la física y el time scale pero no el del sistema de Unity uno basado en el GameManager que guarda esta referencia. Por otro lado, esta el script de dinopostre el cual no esta al mismo nivel que el del jugador siendo un objeto hijo del mismo para poder cambiarlo cuando el jugador cambiara su dinosaurio pero eso es un comportamiento que se vera mas adelante. 
	
  Este script se llama dinopostre y este maneja todo los stats y comportamientos generales que puede tener estos piezas de repostería, no tiene sus stats por default cargados en cada prefab de ellos, estos están pre definidos y cargados por medio de un scriptable object que maneja sus stats base junto con sus stats máximos los cuales este comportamiento carga en base al nivel en el que está haciendo una función lerp para determinar su nivel de poder al nivel actual del dino para identificar de cual era perteneciente existe un enum que enlista todos los dinos descritos en el juego, estos cumplen su función de identificador a través de las consultas. En este mismo script también hice un lista publica con las habilidades de ldino de manera publica para darle unas habilidades por default para el menejo del jugador clasificándolos en dos tipos los especiales y físicos. 

*	Los ataques especiales se basan en generación de partículas
*	Los ataques físicos tienen como base la activación y desactivación de un trigger
  
  Por este motivo también cree un script de attack object que es el que se encarga de enviar las señales de daño entre los dinos, haciendo que cada prefab de dino tuviera un listado de habilidades con un gameobjects ligado que activa y desactiva al momento de atacar y a través de un evento avisa al manager de enemigos que alguien recibió daño al no tener acceso a los scripts controladores o dinopostres por cuestión de arquitectura cree un delegate en el Enemy manager en donde todos los enemigos se registran y salen al morir para hacer una revisión de quien fue lastimado por medio de su ID designado por unity.

### Los enemigos
  
  Una vez logrado el script del jugador, empecé a desarrollar la inteligencia de los enemigos, lo cuales tienen una inteligencia bastante primitiva, esta consiste en diferentes estados que son los que son para perseguir al jugador por medio de la proximidad siempre checando que tan cerca está el jugador. SI se encontraba en el rango de visión del jugador se acercaba a él para atacarlo, lo divertido del ataque es que necesitaba un lugar en donde pudiera almacenar los datos base de los enemigos, como son sus prefabs y cuál era su ataque por default, para resolverlo cree un scriptable object que almacenaba esos datos en un inicio. 
  
  Una vez terminado los enemigos empezó a probar el spawner de enemigos, creando una objeto trigger en el cual al entrar en contacto con el jugador creara los enemigos en base a algo muy importante que era el nivel en el que se encuentra, para ello cree el script de Level manager que se encarga de cargar los enemigos que se pueden encontrar en el nivel, junto con un manager de enemigos para que este se encargara de mandar la información del ataque a todos los controladores por medio de una acción delegada.  A su vez agregue una lista de locaciones por dino, la cual define en que lugares puedes encontrar un dino y cuál es la posibilidad con la que aparezca esto permitía no solo crear un tipo de enemigo sino varios.
  
### Recompensas

  Una vez solucionado el aspecto de creación de enemigos toco la mecánica de recompensas, una de las primeras ideas que me vino a la mente fue usar el sistema legacy de unity para crear una partícula con trigger para la recolección pero resultaba ser muy pesada, después me propusieron el otro sistema de partículas que viene con el motor, pero no logre terminar de encontrar una manera de generar estas llamadas cuando el jugador entrara en contacto con la partícula, por lo que por última opción decidí spawnear las recompensas.
  
  Para evitar una saturación de memoria, cree un manager de recompensas el cual se encargaba de crear las recompensas y mantener un registro de cada tipo de recompensas, así si algún enemigo dropeaba una recompensa ya creada mandaba solo volverla a activar, por cuestión de que sabía que si desactivaba el gameobject en un juego online tiene el problema que se desincroniza de la red por lo cual en lugar de hacer eso, desactivaba el componente del render y pasaba al objeto a una capa que ignoraba las colisiones con los enemigos y el jugador únicamente así no se caían en el abismo perpetuo y continuaban exigiendo. 
  
  ![Preview del gameplay](/READMEimg/DinoTest.gif)

### Dispacher
  
  El siguiente reto para solucionar fue el dispacher, como el juego le da la capacidad al jugador de cambiar de dinopostre a voluntad con alguno de los que ya tuviese creado, necesitaba dos cosas, el lugar donde se almacenaran los datos del jugador una clase que llame Playerdata la cual tiene un registro completo de todo lo que tiene el jugador. Y también empecé a crear diversos canvas para los elementos del UI como fue el menú de pausa, los elementos del gameplay y el dispacher. La primera tarea fue mostrar la lista, pero de nuevo me venían flashbacks de mi tiempo jugando Jurassic World the game. Jurassic World the game, tiene un sistema de almacenamiento de dinosaurios para sus combates muy parecido al que quería crear solo que tenía un muy grave problema que eh notado mucho, ahí veces en el que abrir ese menú se ha lenta el juego de manera espontánea, denotando una falta de optimización.
  
  Mi teoría es que el juego crea una UI por cada dino en tu inventario y como estos tienen un tiempo de enfriamiento los ejecuta todos al mismo tiempo creando una lag inmenso por eso yo decidí crear mi menú con la menor cantidad de elementos posibles para evitar este problema con la creación de elementos infinitos. Para solucionarlo hice un sistema que manejara un canvas con una lista predefinida de elementos dentro de un layout para solo mover en forma de un carrusel los elementos el de abajo hasta arriba en caso de estar subiendo y el de arriba para abajo en caso de ir bajando. Esto fue junto con un script que se encargara de almacenar las acciones de los botones cuando este fuera seleccionado y al hacer un input, para eso tuve que hacer una búsqueda de como poder hacer un evento de selección descubriendo así la interfaz de ISelectHandler el cual permitía llamar funciones en la selección, así ya nada más tuve que definir un UnityEvent que guardara esta información y al momento de ser seleccionado el botón regresar al dispacher las función convirtiendo así al dispacher en un nuevo manager sin ser un manager.

### Teletransporte
  
  Para este punto ya era necesario hacer pruebas para cambiar de estancia de juego a las que requerían para pasar a la estancia del juego. Por definición puse que el jugador tiene que pasar por 3 fases antes de entrar en la estancia del boss por lo que tuve que creer objetos trigger para que el jugador tocara y cambia de estancia pero había una situación estos tenían que tener dos tipos los de salida y los de entrada así que compartiendo el mismo componente y dentro del Level Manager definí una lista de teleportadores que se registran al inicio de la escena pero solo aquellos que estuvieran bloqueados esto para definir los que serían de llegada, mientras que los otros no contarían con ninguna condición. 
	
  Para la elección simplemente se escoge un índice al azar al cual el componente del jugador que es identificado por su tag Player es movido a la posición indicado al pasar por los 3 se transportaba al del boss el cual es un teleportador que está bloqueado y esta designado como del boss. Para salir de la fase definí un teleportador que este marcado como de estancia el cual carga la estancia principal y en momentos futuros puse como que solo se activara cuando el boss era derrotado.

### Jefes
  
  Con el teletransporte ya creado ya solo quedaba terminar de hacer una definición de el boss para poder terminar los elementos base del juego, una de las diferencias clave entre un enemigo y un boss es la cantidad de ataques que puede usar, el enemigo normal solo usa un ataque mientras que el boss tiene un listado de estos sin embargo estos dos tienen muchos comportamientos similares entre sí, como se mueven, como pierden vida etc. Así que era lógico emparentarlos, una vez creado empecé a designar la parte de ataques por un delegate definido en la clase del enemigo con lo que después creando una lista de estos elementos en el boss me permitió hacer una lista de selección aleatoria en el boss en base a un random.

### Game Mode

  Para la escena principal tuve que crear un GameMode que ya se encargara de manejar la creación de todos los menús por escena, asiendo un diccionario donde registrara en el Level manager que tipo de escena se acaba de cargar según su nombre, así saber qué tipo es Gamemode cargar dejándolo en tres tipos:
1.	INSTAGE: Este se encarga de cargar todos los elementos necesarios en una estancia como el menú de pasa, el inventario, dispacher y el de gameplay. 
2.	MAP: Este se hereda de INSTAGW pues solo agrega algunas características puntuales como descripciones visuales,
3.	MENU: Este Gamemode carga todos los menus de el inicio para crear, cargar o modificar los settings así como salir de la app.
También se creo una clase base que tenia sus funciones principales como cargar escena, un diccionario con las definiciones de cada parte y un stack de menús abiertos para que estos no se cerraran como se abrían. 

### Mapa
  
  Para el mapa se hicieron modificaciones al script del transportador para cargar escenas específicas creando un almacén de descripción de escenas en donde se especifica asignados por rangos y áreas para que así supera a donde mover al jugador en cada ocasión con eso también se ajustó el botón de salir de fase del menú de pausa para que permitiera al jugador salir y regresar a el menú principal del juego o al a zona de crianza. 
  
  Con eso y para dar una descripción mas especifica de las estancias con un UI que cargaba estas mismas en base a cuál era la zona al que mandaba el transportador al jugador, lo que le daba la información para saber que enemigos podía encontrar ahí y que jefes podían estar. 
  
  Por último agregue con la misma lógica de las descripciones nombres a las construcciones de la zona y todo esto en base a un trigger de rango que activa estas descripción.

### Horno
  
  Para crear nuevas creaturas o eliminarlas se especificó la creación de un sistema de horno donde el jugador puede ir a mejorar sus dinos o crear nuevos. Para empezar se creo este menú de mejoras donde se cargan todos los dinos que se pueden crear así como como sus stats actuales, como compartía unas características similares al carrusel del dispacher decidí romper el código y crear uno nuevo que fuera la base de este. De igual manera este mismo se hereda del dispacher agregando un montón de referencias a sliders de información del dino. 
  
  Lo difícil aquí fue el encontrar como referenciar los ingredientes necesarios para poder subir de nivel pues en primera instancia había un problema con que por una cuestión que extraía la referencia directa del storage que almacenaba esta información al actualizar para el siguiente nivel estas cambiaban en cada iteración, fue hasta que entendí que debía de regresar un objeto nuevo creando una instancia de la clase Ingredient Count y agregándolas a una nueva lista. Después de eso cree el sistema de crecimiento de experiencia definido en 3 variantes:

*	Corto = (nivel/5+1) * valor
*	Medio = ((nivel ^2 *4 )/500+1)*valor
*	Largo = = ((nivel ^3 )/5000+1)*valor

  Con el sistema de subida de nivel resuelto faltaba el de creación, del cual tuvo que salir un sistema de desbloqueo que usaría de manera paralela con otros elementos de las escenas, para hacer esto desglose una lista de todas las características con las que un elemento podría ser desbloqueado, son eso agregue dos listas a los datos del jugador que incluía una lista con el identificador de recetas desbloqueadas basada en el valor del enum de los nombres de los dinos, por lo que no necesite hacer una asignación de un ID para las recetas solo con a que dino pertenencia, el segundo elemento fue el de los triggers en donde se agregan todos los objetos o eventos que se desbloquean al terminar algo en este caso solo tenía la caída del puente para acceder al jefe de estancia.

### Menú 
  
  Para la creación de el menú principal ya se tenia que probar el sistema de creación de archivos de guardado por lo que necesitaba una manera de saber si el archivo esta registrado así que lo hago sabiendo si tiene un nombre puesto esto para que no contara las pruebas que hice anteriormente cuando estaba trabajando en los desbloqueos de recetas y eventos. 
  
  Creando un Memory manager que hace un bynary serealization para guardar la información y cargarla. El menú principal hace una revisión de cuantos archivos de guardados ahí para saber si mostrar el botón de crear un nuevo archivo o cargar partida. 
Para la pantalla de carga hace una copia de los archivos de guardado actualizando su información de jefes derrotados como lo hacen los juegos de pokemon mystery dungeon y asigna un evento de cargar los datos y los manda a el GameManager. En el caso de creación de partidas lo que presenta es un teclado creado por mi en el que llena el nombre de como quieres llamar a tu equipo y una vez seleccionado el jugador le puede dar crear para empezar una nueva partida.
	
  Para este teclado virtual le cree un segundo arreglo de símbolos asignando las letras del teclado original a un char para poder cambiarlos.

## A decorara el pastel
  
  Después de pulir algunos problemas de física, la demo ha quedado decente, no perfecta lamentablemente, pero se ha logrado mucho en el trascurso de estos meses, para terminar con esto decidí cerrar con la texturización de los modelos y empezar a crear animaciones básicas de idle, caminando y corrido para cada uno de los modelos.
Todo esto con su texturizado especial para que hagan referencia a los diversos postres que han representado en mi imaginación.

![Texturas Agujaceratops](/READMEimg/Agujaceratops.png)
![Texturas Microceratops](/READMEimg/Microceratops.png)
![Texturas Eocaecilio](/READMEimg/Eicalio.png)
![Texturas Protarchaeopteryx](/READMEimg/Proterix.png)

## Conclusión
  
  Durante estos 5 meses en el que eh estado trabajando en este proyecto han pasado muchas cosas, cosas buenas y cosas malas, pero ante toda adversidad se logra completar un juego que al menos desde mi punto de vista presenta un ciclo completo de juego. Podría decir que, este es el primer juego que eh hecho solo, aunque quisiera haber llegado mas lejos, yo tenía muchas cosas más que dar la meta original era hacer 12 modelos 3 áreas de juego y el área de jefe, pero me tuve que contener para tener lo que tengo ahora, y como dicen es mejor saber cuando rendirse. 
  
 # Referencias
<br>Autodesk Inc. (2020, April 27). Missing OpenColorIO configuration file. Maya Forum. https://forums.autodesk.com/t5/maya-forum/missing-opencolorio-configuration-file/td-p/9472761
<br>Korotaev, S. (2020, November 25). Unity Assembly Definition Files Tutorial (.asmdef). Let´s Make a Game. https://letsmakeagame.net/assembly-definition-files-tutorial/#:%7E:text=Assembly%20with%20cyclic%20references%20detected,the%20cycle%20in%20some%20way
<br>Markdown: Sintaxis. (2014, February 6). GitHub. https://github.com/ricval/Documentacion/blob/master/Markdown/daringfireball/syntax.md#párrafos-y-saltos-de-línea
<br>Microsoft. (2022a, January 25). Bitwise and shift operators - C# reference. Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
<br>Microsoft. (2022b, January 25). char type - C# reference. Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/char
<br>Microsoft. (2022c, March 28). Directory.GetFiles Method (System.IO). Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=net-6.0
<br>Microsoft. (2022d, March 31). File.Delete(String) Method (System.IO). Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/api/system.io.file.delete?view=net-6.0
<br>Microsoft. (2022e, April 6). Enumerable.Distinct Method (System.Linq). Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinct?view=net-6.0
<br>Microsoft. (2022f, April 6). Enumerable.ToDictionary Method (System.Linq). Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.todictionary?view=net-6.0
<br>Microsoft. (2022g, April 29). BinaryFormatter Class (System.Runtime.Serialization.Formatters.Binary). Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter?view=net-6.0
<br>Microsoft. (2022h, June 2). Array.CopyTo Method (System). Microsoft Docs. https://docs.microsoft.com/en-us/dotnet/api/system.array.copyto?view=net-6.0
<br>Savage, R. (2017). LeanTween. Dented Pixel. http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html#index
<br>Scotese, C. (2022). DinosaurPictures.org - Awesome Dinosaur Pictures. The Dinosaur Database. https://dinosaurpictures.org
<br>Unity Technologies. (2016). MonoBehaviour.OnBecameInvisible. Unity3D. https://docs.unity3d.com/530/Documentation/ScriptReference/MonoBehaviour.OnBecameInvisible.html
<br>Unity Technologies. (2017). Unity - Scripting API: UI.Selectable.OnSelect. Unity 3D. https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.Selectable.OnSelect.html
<br>Unity Technologies. (2018, December 19). Editing Meshes | ProBuilder | 4.3.1. Unity3D. https://docs.unity3d.com/Packages/com.unity.probuilder@4.3/manual/workflow-edit.html
<br>Unity Technologies. (2021, August 12). Set Position (Mesh) | Visual Effect Graph | 12.0.0. Unity 3D. https://docs.unity3d.com/Packages/com.unity.visualeffectgraph@12.0/manual/Block-SetPosition(Mesh).html
<br>Unity Technologies. (2022a, May 27). Grid snapping. Unity3D. https://docs.unity3d.com/Manual/GridSnapping.html
<br>Unity Technologies. (2022b, May 27). Unity - Manual: Building Height Mesh for Accurate Character Placement. Unity 3D. https://docs.unity3d.com/Manual/nav-HeightMesh.html
<br>Unity Technologies. (2022c, May 27). Unity - Manual: Script serialization. Unity 3D. https://docs.unity3d.com/Manual/script-Serialization.html
<br>Unity Technologies. (2022d, May 27). Unity - Scripting API: AnimationCurve. Unity 3D. https://docs.unity3d.com/ScriptReference/AnimationCurve.html
<br>Unity Technologies. (2022e, May 27). Unity - Scripting API: MonoBehaviour.OnBecameVisible(). Unity 3D. https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnBecameVisible.html
<br>Unity Technologies. (2022f, May 27). Unity - Scripting API: MonoBehaviour.OnParticleTrigger. Unity 3D. https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleTrigger.html
<br>Unity Technologies. (2022g, May 27). Unity - Scripting API: Physics.SphereCast. Unity 3D. https://docs.unity3d.com/ScriptReference/Physics.SphereCast.html
<br>Unity Technologies. (2022h, May 27). Unity - Scripting API: WaitWhile. Unity 3D. https://docs.unity3d.com/ScriptReference/WaitWhile.html
