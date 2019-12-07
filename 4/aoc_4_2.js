
function isMatch(n) {
    const digits = n.toString().split('').map(x => Number(x));
    let current = -Infinity;
    let run = 1;

    let adj = false;

    for(let d of digits) {
        if(d < current) {
            return false;
        }

        if(d == current) {
            run++;
        } else {
            if(run == 2) {
                adj = true;
            }
            run = 1;
        }

        current = d;
    }

    if(run == 2) {
        adj = true;
    }

    return adj;
}


let count = 0;


for(let i = 109165; i <= 576723; i++) {
    if(isMatch(i)) {
        count++;
    }
}

console.log(count);