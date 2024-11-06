# Pràctica 2: Portal

## Objectiu

Desenvolupament d’un videojoc estil Portal amb el motor Unity 3D.

## Metodologia

El desenvolupament d’aquesta pràctica es farà en grups de 2 alumnes. No es permetrà l’entrega individual. Cal utilitzar la versió de Unity3D 2022.3.30f1.

## Parts a desenvolupar

- [ ] **Escenari (0.5p)**: Utilitzant els assets pujats a l’eCampus s’haurà de crear un escenari jugable que s’adapti a les necessitats dels següents punts creant un nivell amb gameplay a l’estil Portal (puzles).
- [ ] **Portal Gun (1p)**: El nostre personatge principal tindrà una pistola que generarà portals. Aquests portals només es podran crear en les superfícies vàlides. Hauran d’haver superfícies on no es puguin crear portals. Els portals només es podran crear en posicions on es puguin crear completament sense quedar tallats o bloquejats per algun cap altre objecte o geometria. Amb el primer botó del ratolí es crearà el portal blau i amb el segon botó el taronja Una vegada premut el botó del ratolí, abans de soltar-lo es el portal indicant si es pot crear en aquest espai o no.
- [ ] **Portals (1p)**: Aquests portals hauran de mostrar-se l’altre costat del portal complementari. El player podrà mirar a través d’ells com si fos una finestra. Si el player es mou davant d’aquest la perspectiva haurà de canviar.
- [ ] **Teleport (0.5p)**: El jugador podrà entrar a través dels portals i teletransportar-se d’un portal a altre. La sortida del portal dependrà de l’entrada. Si entrem en diagonal a un portal, haurem de sortir en la mateixa direcció en l’altre portal.
- [ ] **Companion Cubes (1p)**: En l’escena haurà un sortidor de cubs que s’activi cada vegada que polsem un botó. Aquests cubs activaran botons si es posen a sobre d’ells.
- [ ] **Gravity Gun (1p)**: L’arma també haurà de ser capaç d’agafar cubs al fer clic en ells si estan a una certa distància. Aquests hauran de venir cap a l’arma i quedar-se flotant davant d’ella. Al tornar a fer clic el cub sortirà disparat. Si es prem el segon botó del ratolí caurà al terra.
- [ ] **Teleport Cubes (0.5p)**: Si aquests cubs són llançats cap a un portal, aquests hauran de teletransportar-se també i sortir en la mateixa posició i direcció que s’esperaria per l’altre costat i amb la mateixa velocitat.
- [ ] **Resizing (0.5p)**: Una vegada es previsualitza el portal blau abans de col·locar-se, amb la roda del ratolí podrà canviar-se la seva mida des d’un 50% de la seva mida normal fins a un 200%. Els cubs al teletransportar-se entre portals hauran d’adaptar la seva mida a aquestes proporcions. Si es fa un portal blau al 50% i entra un cub normal pel taronja, aquest sortirà al 50% de la seva mida. Si entra pel blau, sortirà al doble de la seva mida.
- [ ] **Turrets (1p)**: També haurà d’haver torretes enemigues. Aquestes llançaran un làser vermell que si el Player el toca morirà a l’instant. Aquestes torretes es desactivaran si li llencem un cub, o si les llencem una altra torreta. Les torretes també podran agafar- se amb el gravity gun. Quan s’agafen es col·locaran en la pistola de tal forma que el làser quedarà disparant cap endavant. Les torretes moriran si les toca el làser d’una altra torreta.
- **_BONUS POINTS_ (màxim 3 punts)** Aquest punt només s’avaluaran si s’han implementat tota la resta de punts. Completa el nivell amb els següents punts:
  - [ ] **Doors / Keys**: Entre sala i sala hauran portes que només es podran obrir al tenir un cub a sobre d’un botó.
  - [ ] **Dead zones**: En l’escenari haurà zones de lava que mataran al Player automàticament si el Player les toca.
  - [ ] **Physic Surfaces**: Algunes superfícies tindran comportaments físics diferents:
    - **a. Bouncing**: Quan un cub/torreta la toqui, rebotat.
    - **b. Sliding**: Quan un cub/torreta la toqui, lliscarà el gel.
    - **c. Destroying**: Quan un cub/torreta la toqui, destruirà.
  - [ ] **Game Over / Retry**: Quan el player mori sortirà una pantalla de Game Over i se li
        donarà la possibilitat de tornar a intentar-ho.
  - [ ] **Checkpoint**: Una vegada superades certes zones, passarem checkpoints que ens
        permetrà si morim continuar la partida des d’aquest punt.
  - [ ] **Sound**: Sonoritzar el joc. Gravity gun, portes, creació de portals, checkpoints, botons,
        mort, torretes, etc.
  - [ ] **Refraction Cube**: En l’escenari haurà llocs amb làsers sortint de la part. Aquests
        làsers podran ser desviats per un refraction cube. El làser reflectit haurà d’arribar a un
        interruptor que permetrà obrir una porta.
  - [ ] **Blocking cube**: Els companion cubes hauran de bloquejar els làsers.
  - [ ] **Crosshair**: El punter del ratolí mostrarà en color el portal ja creat, i buit si encara no
        s’ha posat el portal.

## Entrega

L’entrega es farà penjant un arxiu de text (txt) a la tasca corresponent de l’aula virtual abans de les 23:59 del 12 de Novembre.
Només cal fer una entrega per grup. L’arxiu ha d’incloure:

- Un enllaç (wetransfer) amb la carpeta del projecte de Unity. El nom de l’arxiu a descarregar cal que sigui 3DP2_CognomNom1_CognomNom2.zip.
- Noms dels membres del grup i llistat de les parts desenvolupades
- Enllaç a un vídeo de youtube on es vegin tots els punts desenvolupats. Exemple de video: https://youtu.be/MRsd0ptJxV8

## Avaluació

No s’acceptarà cap entrega fora del termini establert, que no compili, o no compleixi algunes de les condicions especificades a l’apartat entrega (format o contingut del fitxer). Cada part a desenvolupar s’avaluarà amb una de les següents puntuacions:

- 0%: no funciona correctament.
- 50%: funciona correctament.
- 100%: funciona correctament i s’ha implementat seguint les millors pràctiques explicades a classe (codi intel·ligible, reutilitzable, principis SOLID, patrons, etc.) i els recursos elegits, i la implementació i parametrització dels scripts incrementen la jugabilitat global de joc.
