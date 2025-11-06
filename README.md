# Labos et TP pour le cours de Physique A2025 (PGJ1301-65 Physique pour la jouabilité)
## Spaceship Controller

#### Architecture Globale

Les contrôleurs suivent une hiérarchie d'héritage :
- `SpaceshipController1` : Base de navigation
- `SpaceshipController2` : Ajoute la gestion de rotation (hérite de Controller1)
- `SpaceshipController3` : Ajoute le pathfinding (hérite de Controller1)
- `SpaceshipController4` : Ajoute la gestion du payload (hérite de Controller2)

Chaque niveau conserve et étend les fonctionnalités précédentes tout en ajoutant sa propre complexité.

---

Ce projet implémente un système de contrôle de vaisseau spatial à travers 4 niveaux progressifs de complexité. Chaque niveau ajoute des défis supplémentaires nécessitant des solutions physiques et algorithmiques plus avancées.

### Niveau 1 : Navigation de Base
#### Implémentation

**Système de propulsion :**
- Calcul dynamique de la force appliquée basée sur la distance à la cible
- Forces calculées proportionnellement à la distance : `forceY = BASEFORCE + (distance.y * 0.5)`
- Compensation de la gravité (9.81) pour maintenir l'altitude

**Contrôle directionnel :**
- Analyse du vecteur vaisseau-cible pour déterminer quelle paire de moteurs activer
- Activation sélective des moteurs selon l'axe (X ou Y) et la direction nécessaire
- Un seul moteur actif par axe à la fois pour une navigation précise

**Gestion des collisions :**
- Détection des collisions avec l'objectif via `OnCollisionStay`
- Analyse de la normale de contact pour identifier le côté du vaisseau touché
- Application d'une poussée correctrice (150% de la force normale) pour se dégager. Ne prend pas en compte les rebord 
du niveau, prend seulement en compte les collision avec l'obstacle. Cette fonctionnalité est donc seulement utile dans le 
niveau 3.

**Visualisation de débogage :**
- Affichage de la vélocité du vaisseau en temps réel
- Affichage de la puissance de chaque moteur avec des gizmos colorés
- La fonction GetThrustPower() a été ajouté au script RocketEngine.cs. Celle-ci devra donc être réimplémenté pour que la
visualisation fonctionne.

---

### Niveau 2 : Gestion de la Rotation
#### Implémentation

**Initialisation :**
- Application d'une rotation Z aléatoire (0-360°) au démarrage
- Contraintes physiques : position Z verrouillée, rotations X et Y bloquées, rotation Z libre

**Système de sélection dynamique des moteurs :**
- Surcharge de la méthode `ApplyThrust` pour gérer la rotation
- Calcul en temps réel de la world position de chaque moteur
- Détermination de l'orientation actuelle en comparant les positions Y des moteurs
- Identification des moteurs alignés sur l'axe Y vs l'axe X

**Logique adaptative :**
- Si les moteurs 0 et 3 sont sur l'axe Y : sélection basée sur leur position Y relative
- Sinon (moteurs 1 et 2 sur l'axe Y) : logique inversée pour maintenir le contrôle
- Pour chaque direction (haut/bas/gauche/droite), sélection du moteur dont la world position correspond à la direction souhaitée
- Maintien de la compensation gravitationnelle indépendamment de la rotation

---

### Niveau 3 : Contournement d'Obstacles
#### Implémentation

**Détection de l'objectif :**
- Override des méthodes `OnGoalSpawned`, `OnGoalDestroyed` et `OnGoalReached`
- Utilisation de `Physics.OverlapSphere` avec un rayon de 500 unités pour scanner l'environnement
- Filtrage des colliders détectés pour identifier le `GoalComponent`

**Algorithme de pathfinding :**
Méthode `FindPath(Vector3 goalPosition)` :

1. **Détection d'obstacle** : Raycast du vaisseau vers l'objectif
   - Si l'objectif est directement visible → navigation directe
   - Si un obstacle est détecté → calcul de trajectoire alternative

2. **Calcul de points de contournement** :
   - Mesure des extents du collider de l'obstacle
   - Ajout d'une marge de sécurité (2× la largeur du vaisseau)
   - Génération de deux positions alternatives (A et B) perpendiculaires à l'obstacle

3. **Validation des trajectoires** :
   - Raycasts vers chaque point alternatif pour vérifier l'absence d'obstacles
   - Si les deux chemins sont libres : sélection du plus court
   - Si un seul est libre : navigation vers celui-ci

**Optimisations :**
- Queries limitées à `FixedUpdate` pour minimiser les coûts de calcul
- Verrouillage complet de la rotation Z pour simplifier la navigation
- Visualisation des raycasts avec différentes couleurs selon leur fonction

---

### Niveau 4 : Transport de Payload

#### Implémentation

**Détection de l'objectif :**
- Override des méthodes `OnGoalSpawned`, `OnGoalDestroyed` et `OnGoalReached`
- Utilisation de `Physics.OverlapSphere` avec un rayon de 500 unités pour scanner l'environnement
- Filtrage des colliders détectés pour identifier le `GoalComponent`

**Détection et configuration du payload :**
- Recherche du `PayloadComponent` via `Physics.OverlapSphere`
- Configuration du Rigidbody du payload : position Z verrouillée uniquement
- Création dynamique d'un `SpringJoint` si absent

**Configuration du SpringJoint :**
- **Connected Body** : Rigidbody du vaisseau
- **Anchor & Connected Anchor** : Vector3.zero (centre des objets)
- **Spring** : 10.0 (rigidité du ressort)
- **Min Distance** : 2.0 unités (distance minimale maintenue)
- **Max Distance** : 8.0 unités (distance maximale permise)

**Logique de navigation :**
- Le contrôle s'effectue depuis la position du payload plutôt que du vaisseau
- Calcul du vecteur payload-vers-objectif pour déterminer les forces
- Le SpringJoint transmet les mouvements du vaisseau au payload de manière élastique
- Détection continue du PayloadGoal via `Physics.OverlapSphere`

**Visualisation :**
- Debug.DrawLine entre vaisseau et payload (bleu)
- Debug.DrawLine entre payload et objectif (vert)