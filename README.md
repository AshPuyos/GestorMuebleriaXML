# Sistema de Control de Ventas para Mueblería

## Descripción

Este proyecto es un sistema de información diseñado para una mueblería que busca gestionar promociones de muebles de oficina y dormitorio. La aplicación está implementada en C# y permite llevar un control detallado de los clientes y los muebles adquiridos.

Los clientes pueden comprar un solo mueble, ya sea de oficina o de dormitorio, y recibir descuentos según el tipo de mueble: un 10% para muebles de oficina y un 25% para muebles de dormitorio. La información se almacena en archivos XML, incluyendo detalles sobre cada mueble, su estado y el material.

## Características

- **Modelo de Clases**: Desarrollo de un modelo de clases que representa la lógica del sistema.
- **Almacenamiento en XML**: Implementación de repositorios de datos utilizando archivos XML.
- **Interfaz Gráfica MDI**: Diseño de una interfaz amigable para el usuario con formularios ABM, DataGridView y combos.
- **Control de Ventas**: Gestión de la adquisición de muebles y cálculo de descuentos aplicables.
- **Informes**: Generación de informes sobre el mueble más y menos vendido, así como el monto total recaudado por cada categoría.
- **Validación**: Uso de expresiones regulares para validar los campos en el formulario de clientes.
- **Herencia y Polimorfismo**: Aplicación de jerarquías de herencia y polimorfismo en las clases del sistema.
- **Estados de Muebles**: Gestión del estado de los muebles (disponible/adquirido).

## Tecnologías Utilizadas

- C# .NET Framework
- XML
- Visual Studio
