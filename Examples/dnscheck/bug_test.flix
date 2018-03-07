enum Color {
    case Red,
    case Blue
}

enum Size {
    case Small,
    case Medium,
    case Large
}
enum Shape {
    case Square(Color),
    case Triangle(Color),
    case Oval(Color)
}

def same_color(c1:Color,c2:Color) : Bool =
    c1 == c2

def shape_color(c:Shape) : Color =
    match (c) with {
        case Square(c) => c
        case Triangle(c) => c
        case Oval(c) => c
    }

def shape_same_color(c:Shape, c2:Color) : Bool = 
    shape_color(c) == c2


rel Objects(s:Shape, x:Size)
rel BlueObjects(s:Shape)
// The following causes bug:
//    BlueObjects(s) :- Objects(s,x), same_color(shape_color(s),Blue).
    BlueObjects(s) :- Objects(s,x), shape_same_color(s,Blue).


Objects(Triangle(Red)).
Objects(Oval(Blue)).
Objects(Square(Red)).
Objects(Square(Blue)).