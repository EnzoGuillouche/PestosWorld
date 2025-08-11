# PestosWorld

A game made with Unity where you manage a village of penguins, called Pestos.

## Game Overview

You begin the game with only 2 Pestos, and you'll need to manage their happiness to make them reproduce. Let's build the larger village possible!

### Happiness

Happiness is the way Pestos feel.
With a low happiness level, they are going to be less efficient in tasks, sleep more, etc.
On the other hand, with a high happiness level, Pestos are going to be curious, creative, efficient. And then, they will reproduce reaching a certain level of happiness.

#### Factors

Happiness in PestosWorld comes from four main factors:

**Curiosity** – the urge to explore and discover new places.
**Creativity** – the drive to invent, build, and decorate.
**Comfort** – the sense of safety, warmth, and personal space.
**Socialization** – the joy of interacting and forming bonds with others.

The happiness' factors can be increased when Pestos perform actions.
Those factors will be needed to perform actions.

#### Relationships

The factors are connected to one another.
Performing actions will raise certain factors while sometimes lowering others — balancing them is key to keeping your Pestos happy and productive.

| Increase →      | Curiosity | Creativity | Comfort | Socialization |
| --------------- | --------- | ---------- | ------- | ------------- |
| Curiosity ↑     | —         | +Small     | -Medium | -Small        |
| Creativity ↑    | +Small    | —          | -Small  | ±Small        |
| Comfort ↑       | -Medium   | -Small     | —       | +Small        |
| Socialization ↑ | -Small    | ±Small     | +Small  | —             |

Here, the *±Small* means it could be positive or negative depending on context — for example, a "collaborative creative action" might raise both Socialization and Creativity, while "solo inventing" would lower Socialization.

### Actions

You can tell Pestos to do actions. Actions will require a certain level of factors to be done.
E.g: explore a cave.

This action can be performed with a certain level of curiosity.
It will increase mineral materials (See `Materials` section).
Although, exploring a cave will decrease Comfort and Socialization.

#### Automatic actions

When your village grow, some actions become automatic. You will be able to assign roles/jobs to Pestos and they will perform actions depending on what you told them to do.
E.g: you assign the `Farmer` role to a Pesto. He will work in his farm automatically without you telling him to do it.

### Materials

Pestos can gather and use materials to grow and improve their village. When a Pesto has enough Curiosity, they can explore new areas.

Different locations yield different resources:

- **Caves** – minerals like *iron*, *coal*, and *rare crystals*.
- **Rivers and Lakes** – *fresh water*, *salt*, and *food*.
- **Forests** – *wood*, *herbs*, *food* and *wildlife products*.
- **Coastal Areas** – *food*, *shells*, and special *ocean minerals*.

Once resources are collected, Creativity comes into play. With enough Creativity, Pestos can invent and design new items, tools, or structures. The invention process requires both:

- A specific set of materials.
- A Creativity level high enough to unlock the idea.

After something has been invented, it can be crafted again by any Pesto as long as they have the required materials — no Creativity needed for repeated production.
