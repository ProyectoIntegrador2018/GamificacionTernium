using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// Clase para el manejo de preguntas tipo de decision
public class QuestionManager : MonoBehaviour {

    // Para manipular el Texto que se despliega
    public Text CanvasText;

    // Para manipular los botones
    public Button Btn1;
    public Button Btn2;
    public Button Btn3;
    public string Escena;

    //public int SegundosEspera;

    // Las opciones de los botones, esto es para control mas abajo
    bool Opt1;
    bool Opt2;
    bool Opt3;

    // Vidas que se van a quitar dependiendo del error
    public int vidasFallo1;
    public int vidasFallo2;

    // Esto es para que podamos definir a que pregunta brincamos, gracias al index, solo tenemos que ponerlas en el orden correcto
    // Esta es la escena que sigue, mas abajo se define cual sera
    string SigEscena;

    // Este es el camino correcto
    public string SigEscenaCorrecto;

    // Este es el camino por Arriba, checar Moqup
    public string SigEscenaError1;

    // Este es el camino por Abajo, checar Moqup
    public string SigEscenaError2;

    // Esto es para cargar la pregunta correcta
    //public int PreguntaActual;

    // Esta es clase Preguntas, esta aqui porque importarla tiene que ver con Visual Studio, entonces para facilitarlo la puse aqui 
    public class Preguntas {

        // Constructor that takes no arguments:
        public Preguntas() {
            Pregunta = "Aqui va la pregunta";
            Opc1 = "a";
            Opc2 = "";
            Vidas1 = 0;
            Vidas2 = 0;
            Opc3 = "";
            Fail1 = "";
            Fail2 = "";
            Correct = 0;
            Points = 0;


        }

        // Constructor with arguments
        public Preguntas(string pregunta, string opc1, string opc2, int vidas1, int vidas2, string opc3, string fail1, string fail2, int correct, int points) {
            Pregunta = pregunta;
            Opc1 = opc1;
            Opc2 = opc2;
            Vidas1 = vidas1;
            Vidas2 = vidas2;
            Opc3 = opc3;
            Fail1 = fail1;
            Fail2 = fail2;
            Correct = correct;
            Points = points;
        }

        // Auto-implemented readonly property:
        public string Pregunta { get; }
        public string Opc1 { get; }
        public string Opc2 { get; }
        public int Vidas1 { get; }
        public int Vidas2 { get; }
        public string Opc3 { get; }
        public string Fail1 { get; }
        public string Fail2 { get; }
        public int Correct { get; }
        public int Points { get; }
    }

    // Los estados del juego, esto lo aprendi del video que estaba en la carta
    private enum States {
        Questions, trueState, falseState, falseState2
    };

    // TODAS las preguntas, las cargamos aqui luego las cargamos, dependiendo de la pregunta que sea necesitada
    
        //Desde aqui las preguntas estan mal
        //ES1
    Preguntas ES1Q1 = new Preguntas(
        "Se informa a Guardia de daño en cuerpo de rodillo",
        "Siempre sucede, ya sé que hacer",
        "Solicito me informen todo el detalle por radio",
        4,
        1,
        "Verifico personalmente en el área, el daño del rodillo, tomando las medidas de seguridad adecuadas",
        "Incorrecta interpretación del problema y provocar preparación inadecuada",
        "Minimizar el problema y provocar mayor afectación",
        3,
        100);
    Preguntas ES1Q3 = new Preguntas(
        "Te encuentras en el área, deberas de...",
        "Atender de inmediato, interviniendo el equipo en cuanto deje de moverse",
        "No informar la anormalidad y dejar que alguien mas se encargue",
        3,
        2,
        "Llenar el APR, realizar bloqueos efectivos e identificar las actividades necesarias para la solución",
        "Posible daño permanente al equipo al no estar identificando el problema. Se debería revisar detalladamente.",
        "El tiempo de demora puede extenderse más de lo estrictamente necesario. Se debería revisar detalladamente.",
        3,
        100);
    Preguntas ES1Q4 = new Preguntas(
        "El guardia recibe más información sin ir al área",
        "None",
        "Va directamente a solucionar el problema",
        0,
        3,
        "Va al área afectada",
        "None",
        "El guardia debe evaluar el area y llenar el APR.",
        2,
        100);
    Preguntas ES1Q5 = new Preguntas(
        "Después de la evaluación, deberás de...",
        "Solucionar el problema",
        "Ignorar el problema y asumir que no habrá afectación",
        1,
        2,
        "Llenar el APR",
        "Debe de llenar el APR antes de continuar.",
        "Minimiza el problema y provoca mayor afectación. Debe llenar el APR y después solucioanr el problema antes de generar el aviso.",
        3,
        100);
    Preguntas ES1Q6 = new Preguntas(
        "Despues de corregir el daño",
        "Informo a Calidad",
        "Genero una orden de Mantenimiento",
        1,
        1,
        "Genero un aviso",
        "Calidad no es responsable de esta parte del proceso. Se debe generar un aviso.",
        "Se generaría una orden de mantenimiento sin información completa del problema. Se debe generar un aviso.",
        3,
        100);
    Preguntas ES1Q7 = new Preguntas(
        "¿Qué tipo de aviso debo generar?",
        "Aviso M0",
        "Aviso M3",
        1,
        1,
        "Aviso M2",
        "Este aviso surge sólo de inspecciones, se debe generar el M2.",
        "Este aviso se utiliza sólo para guardar información de actividad en el equipo, se debe generar el M2.",
        3,
        100);
    Preguntas ES1Q8 = new Preguntas(
        "¿Qué campos deben ser llenados en el Aviso M2?",
        "Colocar la palabra 'vinculos' en algun campo",
        "Agregar multimedia",
        1,
        1,
        "Equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte, objeto, sintoma, avería, causa y sus textos",
        "Esta palabra no informa el detalle de lo ocurrido. Debe llenar: equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte, objeto, sintoma, avería, causa y sus textos.",
        "Es opcional agregar alguna foto al aviso. Es obligatorio llenar: equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte, objeto, sintoma, avería, causa y sus textos.",
        3,
        100);
    //ES2
    Preguntas ES2Q1 = new Preguntas(
        "En un recorrido por el área, el guardia escucha un ruido anormal", 
        "Es la hora de la comida y llevo prisa, continuo mi camino sin prestar atención", 
        "Alguien debe estar haciendo alguna actividad", 
        1, 
        3, 
        "Tomando las precauciones de seguridad, identifico la fuente del ruido", 
        "Minimiza el problema y provoca mayor afectación.", 
        "Incorrecta interpretación del posible problema.", 
        3, 
        100);
    Preguntas ES2Q2 = new Preguntas(
        "Al identificar la fuente", 
        "Trato de sentir alguna vibración con la mano", 
        "No es tan fuerte o raro el ruido, continuo con mi día", 
        1, 
        2, 
        "Evaluó el daño, utilizando los 5 sentidos de forma segura", 
        "Riesgo de accidente al tocar equipo en movimiento.", 
        "Minimizo el problema y provoco mayor afectación.", 
        3, 
        100);
    Preguntas ES2Q4 = new Preguntas(
        "Una vez evaluado el problema", 
        "Atender de inmediato, interviniendo el equipo en cuanto deje de moverse", 
        "No informar la anormalidad y dejar que alguien más se encargue", 
        1, 
        2, 
        "Llenar el APR, realizar bloqueos efectivos e identificar las actividades necesarias para la solución", 
        "Riesgo de accidente al no realizar el bloqueo efectivo.", 
        "Minimizo el problema y provoco mayor afectación.", 
        3, 
        100);
    Preguntas ES2Q6 = new Preguntas(
        "Después de solucionado el daño", 
        "Genero una orden de Mantenimiento", 
        "Informo a Programación para que indique el paro de línea", 
        4, 
        4, 
        "Genero un aviso", 
        "Orden de mantenimiento sin información completa del problema.", 
        "Programación no es responsable de esta parte del proceso.", 
        3, 
        100);
    Preguntas ES2Q7 = new Preguntas(
        "¿Qué tipo de aviso debo generar?", 
        "Aviso MB", "Aviso M0", 
        3, 
        3, 
        "Aviso M2, sin tilde de parada", 
        "Este aviso se utiliza para solicitar actividades a un sector diferente al propio.", 
        "Este aviso lo genera el inspector.", 
        3, 
        100);
    Preguntas ES2Q8 = new Preguntas(
        "¿Que campos deben ser llenados en el aviso M2?", 
        "Colocar la palabra 'vínculos' en algún campo", 
        "Agregar multimedia", 
        2, 
        2, 
        "Equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte, objeto, síntoma, avería, causa y sus textos", 
        "Esta palabra no informa el detalle de lo ocurrido.", "Es opcional agregar alguna foto al aviso.", 
        3, 
        100);
    Preguntas ES2Q9 = new Preguntas(
        "¿En caso de colocar 'otros' como síntoma de avería o causa ¿qué debo colocar en texto?", 
        "Colocar palabras clave o abreviadas", 
        "Colocar el tag del equipo", 
        1, 
        1, 
        "Agregar el texto con el detalle del problema", 
        "No todas las personas reconocen el significado de nuestras abreviaciones. Hay que agregar el texto con el detalle del problema.", 
        "Puede ser un dato no rastreable en el sistema. Hay que agregar el texto con el detalle del problema.", 
        3, 
        100);
    Preguntas ES2Q10 = new Preguntas(
        "Sigo caminando y me encuentro al lado de una máquina dañada, ¿qué hago?", 
        "None", 
        "Alguien más se encargará y tengo mucha hambre", 
        5, 
        5, 
        "Dado que no traigo equipo de seguridad, hago una evaluación rápida con mis 5 sentidos", 
        "None", 
        "Negligencia de mis actividades como guardia.", 
        2, 
        100);
    Preguntas ES2Q11 = new Preguntas(
        "Otro guardia está haciendo una reparación y me percato que la máquina sigue operando, ¿qué hago?", 
        "None", 
        "La actividad no debe tener nada que ver con la máquina", 
        4, 
        4, 
        "Busco que se haga un bloque efectivo de la maquina", 
        "None", 
        "Incorrecta interpretación del posible problema y riesgo de accidente al no realizar el bloqueo efectivo.", 
        2, 
        100);
    //ES3
    Preguntas ES3Q1 = new Preguntas(
        "Durante la inspección se detecta una bomba con sobrecalentamiento", 
        "Hoy hace mucho calor, es por eso que está por encima de su máximo", 
        "Está muy poco por arriba del máximo establecido, sí soporta", 
        3, 
        3, 
        "Verifico nuevamente la temperatura con el equipo específico para rectificar el valor", 
        "Posible daño por sobrecalentamiento, el valor del punto de medida ya considera la temperatura ambiente.", 
        "Minimizo el problema y posible daño irreparable en la bomba.", 
        3, 
        100);
    Preguntas ES3Q2 = new Preguntas(
        "Al identificar el sobrecalentamiento", 
        "Toco con la mano para verificar qué tan caliente está", 
        "Esta bomba no es importante para el proceso", 
        1, 
        2, 
        "Busco la posible causa del sobrecalentamiento", 
        "Riesgo de accidente al tocar equipo en funcionamiento.", 
        "Minimizo el problema y provoco mayor afectación.", 
        3, 
        100);
    Preguntas ES3Q4 = new Preguntas(
        "¿Qué tipo de aviso debo generar?", 
        "M5", 
        "M6", 
        1, 
        1, 
        "M0", 
        "Este aviso es para Ingeniería de Mantenimiento para mejora en equipo o proceso.", 
        "Se utiliza para enviar a reparar a talleres.", 
        3, 
        100);
    Preguntas ES3Q5 = new Preguntas(
        "¿Qué tipo de orden debo asociar?", 
        "PM10", 
        "PM12", 
        1, 
        1, 
        "PM11", 
        "Esta orden es para reparación de emergencia.", 
        "Esta orden es para reparación por parte de talleres.", 
        3, 
        100);
    Preguntas ES3Q6 = new Preguntas(
        "¿Lo reportas?", 
        "None", 
        "No", 
        1, 
        1, 
        "Sí", 
        "None", 
        "Minimizo el problema y posible daño irreparable en la bombas.", 
        2, 
        100);
    //ES4
    // public Preguntas(string pregunta, string opc1, string opc2, int vidas1, int vidas2, string opc3, string fail1, string fail2, int correct, int points)
    Preguntas ES4Q1 = new Preguntas(
        "Operaciones reporta paro de línea después de un movimiento inusual (golpe a equipo) del material", 
        "Forzar mecánica o electrónicamente la línea para que continúe en operación", 
        "Operaciones se debe encargar del problema", 
        1, 
        1, 
        "Tomo medidas de seguridad adecuadas e ir a revisar el área donde hubo el movimiento", 
        "Posible daño permanente al equipo al no estar identificando el problema. Se debe tomar medidas de seguridad y luego revisar el área.", 
        "Es Mantenimiento el responsable del equipo, puede solicitar apoyo a Operaciones para la solución. Se debe tomar medidas de seguridad y luego revisar el área.", 
        3, 
        100);
    Preguntas ES4Q3 = new Preguntas(
        "Identificar la causa", 
        "Revisar superficialmente y entregar la línea a Operaciones para su arranque sin realizar alguna reparación", 
        "Solicitar todos los componentes que pudieran haberse dañado y no intervenir hasta no tenerlos físicamente", 
        2, 
        1, 
        "Revisar detalladamente indicios de objetos golpeados, desacomodados o sueltos", 
        "Posible daño permanente al equipo al no estar identificando el problema. Se debería revisar detalladamente.", 
        "El tiempo de demora puede extenderse más de lo estrictamente necesario. Se debería revisar detalladamente.", 
        3, 
        100);
    Preguntas ES4Q6 = new Preguntas(
        "Después de solucionado el daño", 
        "Genero una orden de mantenimiento", 
        "Informo a Programación para que indique el paro de línea", 
        2, 
        2, 
        "Genero un aviso", 
        "Orden de mantenimiento sin información completa del problema. Se debe generar un aviso.", 
        "Programación no es responsable de esta parte del proceso. Se debe generar un aviso.", 
        3, 
        100);
    Preguntas ES4Q7 = new Preguntas(
        "¿Qué tipo de aviso es el correcto en este caso?", 
        "Aviso MB", 
        "Aviso M0", 
        1, 
        1, 
        "Aviso M2", 
        "Este aviso se utiliza para solicitar actividades a un sector diferente al propio. Debería ser un aviso M2.", 
        "Este aviso lo genera el inspector. Debería ser un aviso M2.", 
        3, 
        100);
    Preguntas ES4Q8 = new Preguntas(
        "¿Qué campos deben ser llenados en el aviso M2?", 
        "Colocar la palabra Vínculos en algún campo", 
        "Agregar multimedia", 
        1, 
        1, 
        "Equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte del objeto, síntoma de avería y causa", 
        "Esta palabra no informa el detalle de lo ocurrido. Se debe llenar los campos: Equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte del objeto, síntoma de avería y causa.", 
        "Es opcional agregar alguna foto al aviso. Se debe llenar los campos: Equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte del objeto, síntoma de avería y causa.", 
        3, 
        100);
    Preguntas ES4Q9 = new Preguntas(
        "En caso de colocar 'OTROS' como Sint.avería o Causa ¿Qué debo colocar en Texto?", 
        "Colocar palabras clave o abreviadas", 
        "Colocar el tag del equipo", 
        1, 
        1, 
        "Agregar el texto con el detalle del problema", 
        "No todas las personas reconocen el significado de nuestras abreviaciones. Hay que gregar el texto con el detalle del problema.", 
        "Puede ser un dato no rastreable en el sistema. Hay que gregar el texto con el detalle del problema.", 
        3, 
        100);
    //ES5
    Preguntas ES5Q1 = new Preguntas(
        "El nivel 2 reporta paro de línea por sobrecarga de motor", 
        "Solicito me informen todo el detalle por radio", 
        "Restablecer el motor inmediatamente", 
        1, 
        4, 
        "Revisar el motor que indico la falla, tomando las medidas de seguridad adecuadas", 
        "Mantenimiento debe revisar la causa y solicitar apoyo a producción en caso de ser necesario.", 
        "Se debe revisar la causa de la falla antes de restablecer el equipo.", 
        3, 
        100);
    Preguntas ES5Q2 = new Preguntas(
        "Al identificar la causa", 
        "Si está sobrecalentado enfriar con agua", 
        "Desarmar el motor para revisarlo por dentro", 
        4, 
        2, "Reestablecer el motor en caso de que no haya sufrido algún daño", 
        "Puedes causar posible daño permanente al equipo al no estar identificado el problema.", 
        "El tiempo de la demora puede extenderse más de lo estrictamente necesario.", 
        3, 
        100);
    Preguntas ES5Q3 = new Preguntas(
        "¡Oh no! La maquinaria no parece responder, ¿qué sigue?", 
        "Declarar un paro general de operaciones", 
        "Pedir ayuda en repararlo en ese momento", 
        1, 
        1, 
        "Generar un aviso", 
        "Puedes causar más daño en el área de trabajo si tomas decisiones apresuradas.", 
        "Sin instrucciones claras, podrías dañar el equipo o gastar mucho tiempo en su reparo.", 
        3, 
        100);
    Preguntas ES5Q4 = new Preguntas(
        "Después de solucionado el problema", 
        "Generar una orden de Mantenimiento", 
        "Informar a Programación para que indique el paro de línea", 
        1, 
        1, 
        "Generar un aviso", 
        "Orden de mantenimiento sin información completa del problema.", 
        "Programación no es responsable de esta parte del proceso.", 
        3, 
        100);
    Preguntas ES5Q5 = new Preguntas(
        "¿Qué tipo de aviso es el correcto en este caso?", 
        "Aviso MB", 
        "Aviso M0", 
        1, 
        1, 
        "Aviso M2, con tilde de parada", 
        "Este aviso se utiliza para solicitar actividades a un sector diferente al propio.", 
        "Este aviso lo genera el inspector.", 
        3, 
        100);
    Preguntas ES5Q6 = new Preguntas(
        "¿Qué campos deben ser llenados en el Aviso M2?", 
        "Colocar la palabra 'vínculos' en algún campo", 
        "Agregar multimedia", 
        1, 
        1, 
        "Equipo, ¿qué pasó?, ¿por qué pasó?, ¿qué se hizo?, parte del objeto, síntoma de avería y causa", 
        "Esta palabra no informa el detalle de lo ocurrido.", 
        "Es opcional agregar alguna foto al aviso.", 
        3, 
        100);
    Preguntas ES5Q7 = new Preguntas(
        "En caso de colocar 'OTROS' como Sint.avería o Causa ¿Qué debo colocar en Texto?", 
        "Colocar palabras clave o abreviadas", 
        "Colocar el tag del equipo", 
        1, 
        1, 
        "Agregar el texto con el detalle del problema", 
        "No todas las personas reconocen el significado de nuestras abreviaciones. Hay que gregar el texto con el detalle del problema.", 
        "Puede ser un dato no rastreable en el sistema. Hay que gregar el texto con el detalle del problema.", 
        3, 
        100);
    //Hasta aqui
    //ES6
    Preguntas ES6Q1 = new Preguntas(
        "!Operaciones ha reportado aceite tirado en el carro portarollos!",
        "Acercarme al área con el aceite que tengo disponible y rellenar",
        "Solicito me informen todo el detalle por radio",
        1,
        3,
        "Revisar la fuente de la fuga, tomando las medidas de seguridad adecuadas",
        "El aceite podría ser inadecuado para el equipo",
        "Se debe revisar la fuga para tomar las medidas adecuadas",
        3,
        100);
    Preguntas ES6Q2 = new Preguntas(
        "Al identificar la fuente fuga...",
        "Esperar el repuesto",
        "Solicito me informen todo el detalle por radio",
        1,
        1,
        "Contener el derrame con los insumos adecuados",
        "Se debe validar esta opción con Producción",
        "El tiempo de la demora puede extenderse más de lo estrictamente necesario",
        3,
        100);
    Preguntas ES6Q3 = new Preguntas(
        "Si la fuga es por alguna manguera dañada...",
        "Colocar un parche",
        "Usar alguna manguera que tenga disponible",
        2,
        2,
        "Solicitar y/o conseguir una manguera igual para sustituir la dañada",
        "Se debe validar esta opción con Producción",
        "El tiempo de la demora puede extenderse más de lo estrictamente necesario",
        3,
        100);
    Preguntas ES6Q4 = new Preguntas(
        "Si la fuga es por mala sujeción...",
        "Colocar un parche",
        "Usar alguna manguera que tenga disponible",
        2,
        2,
        "Solicitar y/o conseguir una manguera igual para sustituir la dañada",
        "Solución no adecuada",
        "Esta manguera podría no ser la adecuada para el funcionamiento del equipo",
        3,
        100);
    Preguntas ES6Q5 = new Preguntas(
        "Después de reparada la fuga...",
        "Retirarse de inmediato",
        "Dejar la manguera dañada o un lado del equipo que falló para que los demás se enteren",
        2,
        1,
        "Validar que no fugue más y limpiar el área con los insumos adecuados",
        "Se debe verificar que ya no se presente la fuga",
        "Incumplimiento al orden y limpieza del área",
        3,
        100);
    Preguntas ES6Q6 = new Preguntas(
        "¿Qué tipo de aviso debo generar?",
        "Aviso M1",
        "Aviso M0",
        1,
        1,
        "Aviso M2, sin tilde de parada",
        "Este es un aviso de mantenimiento que solicita actividades a un sector diferente al propio",
        "Este aviso se utiliza para cuando se encuentra algo fuera de condiciones en la Inspección",
        3,
        100);
    Preguntas ES6Q7 = new Preguntas(
        "¿Qué campos deben ser llenados en el Aviso M2?",
        "Colocar la palabra \"Vínculos\" en algún campo",
        "Agregar multimedia",
        1,
        1,
        "Equipo, ¿Qué pasó?, ¿Por Qué pasó?, ¿Qué se hizo?, PARTE OBJETO, SINT.AVERIA y CAUSA",
        "Esta palabra no informa el detalle de lo ocurrido",
        "Es opcional, agregar alguna foto al aviso",
        3,
        100);
    Preguntas ES6Q8 = new Preguntas(
        "En caso de colocar \"OTROS\" como Sint.avería o Causa ¿Qué debo colocar en Texto?",
        "Colocar palabras clave o abreviadas",
        "Colocar el TAG del equipo",
        1,
        1,
        "Agregar el texto con el detalle del problema",
        "No todas las personas reconocen el significado de nuestras abreviaciones",
        "Puede ser un dato no rastreable en Sistema",
        3,
        100);

    //ES7
    Preguntas ES7Q1 = new Preguntas(
        "Operaciones reporta una fuga de aceite hidráulico en un equipo", 
        "Acercarme al área con el aceite que tengo disponible y rellenar", 
        "Solicito me informen todo el detalle por radio", 1, 1, 
        "Revisar la fuente de la fuga, tomando las medidas de seguridad adecuadas", 
        "El aceite podría ser inadecuado para el equipo", 
        "Se debe revisar la fuga para tomar las medidas adecuadas", 3, 100);
    Preguntas ES7Q2 = new Preguntas(
        "Al identificar la fuente fuga", 
        "Solicitar a producción el paro de línea", 
        "Esperar el repuesto ", 1, 1, 
        "Revisar personalmente tomando en cuenta las medidas  de Seguridad el componente o equipo reportado", 
        "Se debe validar esta opción con Producción", 
        "El tiempo de la demora puede extenderse mas de lo estrictamente necesario", 3, 100);
    Preguntas ES7Q3 = new Preguntas(
        "Si la fuga es por alguna manguera dañada:", 
        "Colocar un parche de manera provisional", 
        "Usar alguna manguera que tenga disponible", 1, 1, 
        "Solicitar y/o conseguir una manguera igual para sustituir la dañada", 
        "Solución no adecuada", 
        "Esta manguera podría no ser la adecuada para el funcionamiento del equipo", 3, 100);
    Preguntas ES7Q4 = new Preguntas(
        "¿Qué tipo de aviso debo generar?",
        "Aviso M1", 
        "Aviso M0", 1, 1, 
        "Aviso M2, sin tilde de Parada", 
        "Este es un aviso de mantenimiento que solicita actividades a un sector diferente al propio", 
        "Este aviso se utiliza para cuando se encuentra algo fuera de condiciones en la Inspección", 3, 100);
    Preguntas ES7Q5 = new Preguntas(
        "¿Que campos deben ser llenados en el Aviso M2?", 
        "Colocar la palabra Vínculos en algún campo", 
        "Agregar multimedia", 1, 1, 
        "Equipo, ¿Qué paso?, ¿Por qué paso?, ¿Qué se hizo?, PARTE OBJETO, SINT.AVERIA y CAUSA", 
        "Esta palabra no informa el detalle de lo ocurrido", 
        "Es opcional, agregar alguna foto al aviso.", 3, 100);
    Preguntas ES7Q6 = new Preguntas(
        "En caso de colocar \"OTROS\" como Sint.avería o Causa ¿Qué debo colocar en Texto?", 
        "Colocar palabras clave o abreviadas", 
        "Colocar el TAG del equipo", 1, 1, 
        "Agregar el texto con el detalle del problema", 
        "No todas las personas reconocen el significado de nuestras abreviaciones", 
        "Puede ser un dato no rastreable en Sistema", 3, 100);
    Preguntas ES7Q7 = new Preguntas(
        "Una vez generado el Aviso M2, ¿Que tipo de orden debo generar para la reparación de emergencia?", 
        "Orden PM01", 
        "Orden PM12", 1, 1, 
        "Orden PM10", 
        "Es una orden que se utiliza para mantenimiento preventivo", 
        "Es una orden que se utiliza la reparación en Taller", 3, 100);
    Preguntas ES7Q8 = new Preguntas(
        "¿Que se debe agregar a la orden para solicitar el repuesto?", 
        "El nombre del Supervisor del Área", 
        "El numero de serie del equipo", 1, 1, 
        "Agregar a la orden el componente", 
        "Dato no requerido en la orden", 
        "Dato no requerido en la orden", 3, 100);

    // ES8
        Preguntas ES8Q1 = new Preguntas(
        "Se informa a Inspector de mantenimiento de un aviso M2 abierto", 
        "El aviso debe ser gestionado y respondido por la Guardia", 
        "Personal de operaciones debe dar seguimiento al aviso.", 
        1, 
        1, 
        "Revisar el aviso para informarse de lo reportado", 
        "Si el aviso requiere reparación programada debe ser gestionada por el Inspector", 
        "Es mantenimiento el área responsable del aviso", 
        3, 
        100);
    Preguntas ES8Q2 = new Preguntas(
        "Después de revisar el aviso ¿Qué debería hacer el Inspector?", 
        "Solicitar inmediatamente el repuesto mediante una orden", 
        "Usar algún repuesto que tenga a la mano ", 
        1, 
        1, 
        "Revisar personalmente tomando en cuenta las medidas  de Seguridad el componente o equipo reportado", 
        "Se requiere verificar físicamente lo reportado en el aviso", 
        "Se requiere validar si el repuesto es el adecuado antes de instalarlo", 
        3, 
        100);
    Preguntas ES8Q3 = new Preguntas(
        "Una vez revisado físicamente el Aviso M2, ¿Que tipo de orden debo generar para la reparación programada?", 
        "Orden PM01", 
        "Orden PM12", 
        1, 
        1, 
        "Orden PM11", 
        "Es una orden que se utiliza para mantenimiento preventivo", 
        "Es una orden que se utiliza la reparación en Taller", 
        3, 
        100);
    Preguntas ES8Q4 = new Preguntas(
        "¿Que se debe agregar a la orden para solicitar el repuesto?", 
        "El nombre del Supervisor del Área", 
        "El numero de serie del equipo", 
        1, 
        1, 
        "Agregar a la orden el componente de ser necesario", 
        "Dato no requerido en la orden", 
        "Dato no requerido en la orden", 
        3, 
        100);
    Preguntas ES8Q5 = new Preguntas(
        "Después de realizar el correctivo programado ¿Qué debe realizar el Inspector?", 
        "Nada mas, ya esta corregido el problema", 
        "Avisar por radio a Producción que ya esta corregido el problema", 
        1, 
        1, 
        "Cerrar la orden y generar aviso M3 en caso de requerir guardar historial del equipo", 
        "Se debe dejar registro en Sistema del fin de la actividad, cerrando la orden", 
        "La forma correcta de informar es cerrando la orden, se puede avisar por radio pero NO omitir el cierre de la orden", 
        3, 
        100);

    //ES9
    Preguntas ES9Q1 = new Preguntas(
        "Después de ejecutar una orden ¿Qué tipo de aviso se podría generar?",
        "Aviso M1",
        "Aviso M0",
        5,
        5,
        "Aviso M3",
        "Este es un aviso de mantenimiento que solicita actividades a un sector diferente al propio",
        "Este aviso se utiliza para cuando se encuentra algo fuera de condiciones en la Inspección",
        3,
        100);
    Preguntas ES9Q2 = new Preguntas(
        "¿Cual es la finalidad del aviso M3?",
        "Reportar no conformidades durante una Inspección Programada",
        "Es una aviso para ser revisado por el Taller Central",
        3,
        3,
        "Reportar actividades realizadas sobre los equipos que NO son averías",
        "El aviso que se debe generar en las no conformidades d e Inspección es un M0",
        "El aviso que se utiliza para solicitud a talleres es el M6",
        3,
        100);
    Preguntas ES9Q3 = new Preguntas(
        "¿Cuales serian algunas actividades a reportar en el Aviso M3?",
        "Cambio de algún componente",
        "Una avería que genero demora",
        1,
        1,
        "Limpieza, regulación, llenado de tanque",
        "Es una orden que se utiliza para mantenimiento preventivo",
        "Es una orden que se utiliza la reparación en Taller",
        3,
        100);

    //ES10
    Preguntas ES10Q1 = new Preguntas(
        "Durante la ejecución de una Orden PM01, se detecta un equipo que debe ser reparado ¿Qué tipo de aviso se debe generar?",
        "Aviso M1",
        "Aviso M0",
        1,
        1,
        "Aviso M6",
        "Este es un aviso de mantenimiento que solicita actividades a un sector diferente al propio",
        "Este aviso se utiliza para cuando se encuentra algo fuera de condiciones en la Inspección",
        3,
        100);
    Preguntas ES10Q2 = new Preguntas(
        "¿Cual es la finalidad del aviso M6?",
        "Reportar no conformidades durante una Inspección Programada",
        "Reportar actividades realizadas sobre los equipos que NO son averías",
        1,
        1,
        "Es una aviso para ser revisado por el Taller Central",
        "El aviso que se debe generar en las no conformidades d e Inspección es un M0",
        "El aviso que se utiliza para solicitud a talleres es el M6",
        3,
        100);
    Preguntas ES10Q3 = new Preguntas(
        "¿Que clase de órdenes puede surgir del Aviso M6?",
        "Órdenes PM01 y PM02",
        "Órdenes PM10 y PM11",
        1,
        1,
        "Órdenes PM12 y PM13",
        "Es una orden que se utiliza para correctivo de emergencia",
        "Es una orden que se utiliza para correctivo programado por Mantenimiento",
        3,
        100);
    Preguntas ES10Q4 = new Preguntas(
        "¿Quien genera, gestiona y cierra las órdenes PM12 y PM13?",
        "El Inspector de Mantenimiento",
        "El Supervisor de Guardia",
        1,
        1,
        "Personal del Taller Central",
        "Las órdenes PM12 y PM13 son generadas y gestionadas por personal del Taller Central",
        "Las órdenes PM12 y PM13 son generadas y gestionadas por personal del Taller Central",
        3,
        100);
    Preguntas ES10Q5 = new Preguntas(
        "¿Qué procede? Una vez cerradas las órdenes PM12 y PM13.",
        "Nada mas, ya esta reparado el equipo",
        "Informar al área de Almacén",
        1,
        1,
        "Taller informa a solicitante del aviso del cierre de la orden para que este programe su Instalación",
        "Se debe dejar registro en Sistema del fin de la actividad, cerrando la orden",
        "La forma correcta de informar es cerrando la orden, se puede avisar por radio pero NO omitir el cierre de la orden",
        3,
        100);

    
    

    //Template
    //Preguntas tempD3 = new Preguntas("Pre", "Opc1", "Opc2", 0, 0, "Correcta", "Fail1", "Fail2", 3, 0);
    //Preguntas tempD2 = new Preguntas("P1", "None", "Opc1", 0, 0, "Correcta", "None", "Fail1", 2, 0);
    // Pregunta actual
    Preguntas QA = new Preguntas();

    // Pregunta Error
    Preguntas QE = new Preguntas("ERROR", "ERROR", "ERROR", 0, 0, "ERROR", "ERROR", "ERROR", 3, 0);

    // El estado actual
    private States myState;
  
    // Start is called before the first frame update
    void Start() {
        // Cargar el estado principal

        myState = States.Questions;

        Escena = SceneManager.GetActiveScene().name;

        //Debug.Log(GlobalVariables.Caso);


        // Cargar la pregunta correcta
        switch (Escena) {
            //ES1
            case "P1": QA = ES1Q1; break;
            case "P3": QA = ES1Q3; break;
            case "P4": QA = ES1Q4; break;
            case "P5": QA = ES1Q5; break;
            case "P6.5": QA = ES1Q6; break;
            case "P7": QA = ES1Q7; break;
            case "P8": QA = ES1Q8; break;

            //ES2
            case "ES2P1": QA = ES2Q1; break;
            case "ES2P2": QA = ES2Q2; break;
            case "ES2P4": QA = ES2Q4; break;
            case "ES2P6": QA = ES2Q6; break;
            case "ES2P7": QA = ES2Q7; break;
            case "ES2P8": QA = ES2Q8; break;
            case "ES2P9": QA = ES2Q9; break;
            case "ES2P10": QA = ES2Q10; break;
            case "ES2P11": QA = ES2Q11; break;

            //ES3
            case "ES3P1": QA = ES3Q1; break;
            case "ES3P2": QA = ES3Q2; break;
            case "ES3P4": QA = ES3Q4; break;
            case "ES3P5": QA = ES3Q5; break;
            case "ES3P6": QA = ES3Q6; break;

            //ES4
            case "ES4P1": QA = ES4Q1; break;
            case "ES4P3": QA = ES4Q3; break;
            case "ES4P6": QA = ES4Q6; break;
            case "ES4P7": QA = ES4Q7; break;
            case "ES4P8": QA = ES4Q8; break;
            case "ES4P9": QA = ES4Q9; break;

            //ES5
            case "ES5P1": QA = ES5Q1; break;
            case "ES5P2": QA = ES5Q2; break;
            case "ES5P3": QA = ES5Q3; break;
            case "ES5P4": QA = ES5Q4; break;
            case "ES5P5": QA = ES5Q5; break;
            case "ES5P6": QA = ES5Q6; break;
            case "ES5P7": QA = ES5Q7; break;

            //ES6
            case "ES6P1": QA = ES6Q1; break;
            case "ES6P2": QA = ES6Q2; break;
            case "ES6P3": QA = ES6Q3; break;
            case "ES6P4": QA = ES6Q4; break;
            case "ES6P5": QA = ES6Q5; break;
            case "ES6P6": QA = ES6Q6; break;
            case "ES6P7": QA = ES6Q7; break;
            case "ES6P8": QA = ES6Q8; break;

            //ES7
            case "ES7P1": QA = ES7Q1; break;
            case "ES7P2": QA = ES7Q2; break;
            case "ES7P3": QA = ES7Q3; break;
            case "ES7P4": QA = ES7Q4; break;
            case "ES7P5": QA = ES7Q5; break;
            case "ES7P6": QA = ES7Q6; break;
            case "ES7P7": QA = ES7Q7; break;
            case "ES7P8": QA = ES7Q8; break;

            // ES8
            case "ES8P1": QA = ES8Q1; break;
            case "ES8P2": QA = ES8Q2; break;
            case "ES8P3": QA = ES8Q3; break;
            case "ES8P4": QA = ES8Q4; break;
            case "ES8P5": QA = ES8Q5; break;

            // ES9
            case "ES9P1": QA = ES9Q1; break;
            case "ES9P2": QA = ES9Q2; break;
            case "ES9P3": QA = ES9Q3; break;

            //ES10
            case "ES10P1": QA = ES10Q1; break;
            case "ES10P2": QA = ES10Q2; break;
            case "ES10P3": QA = ES10Q3; break;
            case "ES10P4": QA = ES10Q4; break;
            case "ES10P5": QA = ES10Q5; break;

            default: QA = QE; break;

        }
        
        // Revolver los cuadros de respuesta
        Revolver(QA.Correct); 
    }

    // Update is called once per frame
    void Update() {
        // Como siempre esta corriendo esto, hay que mantenerlo simple, asi que solo cambiara states
        if (myState == States.Questions){ Question(); }
        /*
        else if (myState == States.trueState){ trueState(); }
        else if (myState == States.falseState){ falseState(); }
        else if (myState == States.falseState2) { falseState2(); }
        */
    }

    // Cuando el objeto donde esta el script, es "enabled" comienza a checar esto
    private void OnEnable() {
        // El checar que los botones sean presionados, y que pasa si lo son
        Btn1.onClick.AddListener(delegate {Opt1 = true; Question(); ChangeCurrentScene(); });
        Btn2.onClick.AddListener(delegate {Opt2 = true; Question(); ChangeCurrentScene(); });
        Btn3.onClick.AddListener(delegate {Opt3 = true; Question(); ChangeCurrentScene(); });
    }

    // Desplegar la pregunta actual
    void Question() {
        //Cambiar el texto de la pregunta
        CanvasText.text = QA.Pregunta;

        Btn1.GetComponentInChildren<Text>().text = QA.Opc1;
        Btn2.GetComponentInChildren<Text>().text = QA.Opc2;
        Btn3.GetComponentInChildren<Text>().text = QA.Opc3;

        //Se agrego que se corriera la funcion que lee el estado actual para determinar la siguiente escena porque de lo contrario no se podria cambiar de escena instantaneamente
        if (Opt1 == true) { 
            GameMind.takeAwayLive(QA.Vidas1);
            //GameMind.addPoints(-QA.Points);
            myState = States.falseState;
            falseState();
        } else if (Opt2 == true){
            GameMind.takeAwayLive(QA.Vidas2);
            //GameMind.addPoints(-QA.Points);
            myState = States.falseState2;
            falseState2();
        } else if (Opt3 == true){
            GameMind.addPoints(QA.Points);
            myState = States.trueState;
            trueState();
        }
    }

    // El estado cuando le atina
    void trueState() {
        CanvasText.text = "Correcto!";
        SigEscena = SigEscenaCorrecto;  
    }

    // El estado de error 1
    void falseState() {
        CanvasText.text = "Incorrecto! " +'\n'+ QA.Fail1;
        SigEscena = SigEscenaError1;
    }

    // El estado de error 2
    void falseState2() {
        CanvasText.text = "Incorrecto! " + '\n' + QA.Fail2;
        SigEscena = SigEscenaError2;
    }

    void ChangeCurrentScene() {
        print(SigEscena);
        ColorBlock colors = Btn3.colors;
        colors.normalColor = new Color32(0, 179, 81, 255);
        //colors.highlightedColor = new Color32(0, 179, 81, 255);
        Btn3.colors = colors;

        //Btn3.colors.normalColor = new Color(225, 225, 225, 255); ;
        if (GlobalVariables.lives <= 0) {

            //string Escena = SceneManager.GetActiveScene().name;

            if (Escena.Substring(0, 1) == "P") {
                SceneManager.LoadScene("Lose");
            }
            else {
                SceneManager.LoadScene(Escena.Substring(0, 3) + "Lose");
            }
            //SceneManager.LoadScene("Lose");
        }
        else {
            SceneManager.LoadScene(SigEscena);
        }
    }

    // Es la corutina que espera 5 segundos luego cargan la escena que fue asignada
    //Se comento esta corutina para hacer que el cambio fuera dedicado a un boton en vez de esperar a cierto tiempo
    /*
    IEnumerator WaitSeconds() {

        ColorBlock colors = Btn3.colors;
        colors.normalColor = new Color32(0, 179, 81, 255);
        //colors.highlightedColor = new Color32(0, 179, 81, 255);
        Btn3.colors = colors;

        //Btn3.colors.normalColor = new Color(225, 225, 225, 255); ;
        yield return new WaitForSeconds(SegundosEspera);
        if (GlobalVariables.lives <= 0) {

            //string Escena = SceneManager.GetActiveScene().name;

            if(Escena.Substring(0,1) == "P")
            {
                SceneManager.LoadScene("Lose");
            }
            else
            {
                SceneManager.LoadScene(Escena.Substring(0, 3) +  "Lose");
            }
            //SceneManager.LoadScene("Lose");
        } else {
            SceneManager.LoadScene(SigEscena);
        }
    }
    */

    // Dado que seria mucho problema randomizar la respuesta y el texto 
    // de fallo especifico sera mas facil mover las posiciones de los botones
    void Revolver(int PA) {

        int Rand;
        //New Measures
        // x  = 805
        // y1 = 112
        // y2 = -111
        // y3 = -335

        if (PA == 2) {
            Rand = Random.Range(20, 22);
            Btn1.enabled = false;
            Btn1.gameObject.SetActive(false);
        } else {
            Rand = Random.Range(1, 7);
        }
        
        switch (Rand) {
            case 1:
                Btn1.transform.localPosition = new Vector2(805, 112);
                Btn2.transform.localPosition = new Vector2(805, -111);
                Btn3.transform.localPosition = new Vector2(805, -335);
                break;

            case 3:
                Btn2.transform.localPosition = new Vector2(805, 112);
                Btn1.transform.localPosition = new Vector2(805, -111);
                Btn3.transform.localPosition = new Vector2(805, -335);
                break;

            case 4:
                Btn2.transform.localPosition = new Vector2(805, 112);
                Btn3.transform.localPosition = new Vector2(805, -111);
                Btn1.transform.localPosition = new Vector2(805, -335);
                break;

            case 5:
                Btn3.transform.localPosition = new Vector2(805, 112);
                Btn1.transform.localPosition = new Vector2(805, -111);
                Btn2.transform.localPosition = new Vector2(805, -335);
                break;

            // Estos son para cuando solo hay 2 respuestas
            case 20:
                Btn3.transform.localPosition = new Vector2(805, 50);
                Btn2.transform.localPosition = new Vector2(805, -170);
                break;

            case 21:
                Btn2.transform.localPosition = new Vector2(805, 50);
                Btn3.transform.localPosition = new Vector2(805, -170);
                break;

            default:
                break;
        }
    }

    public static void Help()
    {
        GameObject.Find("Button-2").SetActive(false);//Button-2
    }
}
