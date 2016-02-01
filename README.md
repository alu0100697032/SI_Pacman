# PACMAN Intelligent Agent

Este proyecto ha sido desarrollado para la asignatura de **Sistemas Inteligentes** del itinerario de computación del **_Grado en Ingeniería Informática_** de la **ULL**.

## MIEMBROS DEL GRUPO
Paz Méndez, Germán  ([alu0100503647@ull.edu.es]())  
Hernández Pérez, Víctor ([alu0100503647@ull.edu.es]())

## INTRODUCCIÓN
En este documento se recogen todos los aspectos  descríptivos de desarrollo e implementación del proyecto final propuesto para la asignatura Sistemas inteligentes, desarrollado
a lo largo de todo el curso.


## DESCRIPCIÓN

Elaboración de un **agente inteligente** para el juego de PacMan utilizando diferentes paradigmas inteligentes.

>   
** PacMan ** es un conocido juego en tiempo real que ofrece una interesante plataforma para la investigación.  

#### **PACMAN basado en una Secuencia de movimientos:**  
![](pacman1.JPG)

Definimos un agente inteligente `PacMan` que intentará resolver el laberinto de una manera pre-programada antes de que se inicie el juego. Para ello le indicaremos una secuencia de movimientos que deberá seguir para completar el nivel.

Se ha dicho que siempre es posible obtener **"una programación bien adquirida"** para conseguir un **"objetivo",** por ello intentamos contemplar este paradigma a traves de la posibilidad de establecer uno movimientos iniciales con los que sabemos que el pacman alcanzará la victoria.



#### **PACMAN basado en una tabla de Percepción-Acción:**
![](pacman2.JPG)

Definimos un agente inteligente `PacMan` de reflejo simple que intentará superar el nivel actuando según lo que encuentre a su alrededor. Para ello utilizaremos una tabla de persepción acción como a continuación:    


| UP | RIGHT | DOWN | LEFT | ACTION |
|:------:|:------:|:------:|:------:|:------:|
| WALL | WALL | WALL | WALL  | **STOP**   |
| WALL | WALL | WALL | PILL  | **LEFT**   |
| WALL | WALL | PILL | WALL  | **DOWN**   |
| WALL | PILL | WALL | WALL  | **RIGHT**   |
| PILL | WALL | WALL | WALL  | **UP**   |
| PILL | PILL | WALL | WALL  | **UP**   |
| PILL | PILL | PILL | WALL  | **UP**   |
| PILL | PILL | PILL | PILL  | **UP**   |
| WALL | PILL | PILL | PILL  | **UP**   |
| WALL | WALL | PILL | PILL  | **UP**   |
| WALL | WALL | PILL | PILL  | **UP**   |

Un inconveniente típico de este sistema es que la tabla puede ser enorme y difícil de construir.

####  **PACMAN basado en el algoritmo A*:**
![](pacman3.JPG)


 * Definimos un agente inteligente `PacMan` que reemplazará al ser humano reproduciendo una simplicada versión de juego. El único objetivo será perseguir las bolas de poder.


####  **PACMAN basado en un arbol de comportamiento,**

  ![](pacman4.JPG)


Definimos un agente inteligente `PacMan` adaptativo que "aprende" a traves de un aprendizaje gradual basado en una bateria de test previos para ajustar los parámetros del agente.


Se realizará una comparativa entre los distintos paradigmas inteligentes o de aprendizaje utilizados resolviendo cual es el mejor de todos ellos.


> En 1999 el jugador **Billy Mitchell realizó una partida perfecta de Pac-Man,** entendiéndose como tal una partida en la que el jugador completo los 255 niveles con la puntuación máxima sin ser capturado ni una sola vez. La puntuación máxima es de 3.333.360 puntos.

Estableceremos la posibilidad de modificar los diferentes archivos de configuración del agente creando una salida estadística con los mejores resultados de cada arquitectura utilizada por este. Con esto podremos determinar la mejor arquitectura para realizar una partida perfecta.


## Proyectos similares
#### Wikipedia
+ https://es.wikipedia.org/wiki/Pac-Man
#### Proyectos
#### Vídeos
https://www.youtube.com/watch?v=46hjf_x_0VU  
https://www.youtube.com/watch?v=yfsMHtmGDKEm
https://github.com/MazeSolver/MazeSolver

## RECURSOS
+ C#
+ UNITY
+ BEHAVE
+ Material audiovisual propios del juego original.


+
Imágenes y sonidos propios del juego original.
Programación en Java, Inteligencia Artificial, Minería de Datos.
- BEHAVE





## Tecnologías de IA
* Pre-programación.
* Tablas de Persepción-Acción.
* A*.
* Árbol de comportamiento.

## Desarrollo

## Problemas encontrados

## Funcionamiento
## Conclusiones
En líneas generales hemos cumplido el objetivo y hemos llegado a crear un pacman.


En general, el objetivo ha sido cumplido, y mediante el entrenamiento juega a un nivel casi
humano. Sin embargo, por la cantidad de tiempo requerida por el algoritmo genético
(aproximadamente 5 generaciones por hora con la configuración descrita anteriormente) no se ha
podido comprobar si los bots obtenidos pueden llegar a ser iguales o mejores a un humano.
Como posibles mejores, se podría añadir un sistema de predicción de los movimientos de los
fantasmas, un contador de tiempo de vulnerabilidad de los mismos, detección de los cocos en
pantalla, etc.
