Qué desafíos de la entrega fueron los más difíciles:

- Lograr imprimir un mensaje en la pantalla de un usuario que no ingresa un comando.
- Lograr testear los Handlers, con la implementación del message brindado por Telegram. El inconveniente que teniamos, es que los Handler utilizan un atributo de Message, llamado From, el cual contiene una instancia del usuarion de Telegram que envía el mensaje, y a su vez, From posee una id, que al utilizamos para el funcionamiento del BattleShip. Lo dificil fue detectar el problema, y luego solucionarlo. Para ello descubrimos que se podía instanciar un User de Telegram y asignarlo a Message.
- En determinados momentos, fue complicado detectar que patrón o principio utilizar, sin embargo volvimos a realizar lecturas pasadas en la página de Webasignatura y se nos simplificó la aplicación.

Qué cosas aprendieron enfrentándose al proyecto que no aprendieron en clase como parte de la currícula

- La utilización de varias funcionalidades distintas del bot de Telegram, como imprimir mensajes en usuarios que no ingresan un comando.
- Modificación de los métodos del BaseHandler, principalmente del Método CanHandle, que se encarga de decidir si se toma o no el mensaje enviado por el usuario.

Qué recursos (páginas web, libros, foros, etc) encontraron que les fueron valiosos para sortear los desafíos que encontraron.

- Para la creación de diagramas utilizamos las lecturas de los profesores, principalmente la de herencia y tipos.
- También utilizamos mucho la página guía de C# hecha por Microsoft, está muy bien explicada y tiene mucha infomación de calidad.
- Leimos algunas páginas del libro Object Design.
- Una página que nos ayudo para la utilización de distintas herramientas, patrones y principios fue la página Refactoring Guru. Contiene muy buenas explicaciones y ejemplos de aplicación.
- De todas formas, nos basamos principalmente en las lecturas entregadas por los profesores, tuvieron muchas influencia en la utilización del bot, creación de excepciones propias y correcta distribución de las clases utilizando patrones y principios de diseño.

Y cualquier otro tipo de reflexión, material o comentarios sobre el trabajo en el proyecto.

- La mayor contra que tuvimos, que para ésta entrega logramos remendarla, aún que todavia se puede mejorar, puede no haber aprovechado el tiempo brindado con anticipación, creandonos una sobrecarga en los últimos días de la entrega, a eso hay que sumarle que teníamos entregas y pruebas de otras materias, por lo que el problema fue peor aún. La respuesta a la situación, es hacer lo posible para iniciar proyectos o tareas de alta complejidad lo antes posible, y no confiarse con los rangos de tiempo, por que cuando uno se da cuenta, solo faltan unos días para la entrega y no hay casi nada realizado. En la tercer entrega, ya iniciamos a trabajar desde el momento que hicimos la segunda, por lo que mejoramos muchos en comparación con antes, eso nos permitió hacerle más preguntas a los profesores, quitarnos dudas, y corregir el código con tiempo.
- Para la tercer entrega, tomamos en cuenta todos los comentarios y correcciones de la segunda, e intentamos remendarlos y corregirlos de distintas formas para mejorar lo mejor posible el código.
