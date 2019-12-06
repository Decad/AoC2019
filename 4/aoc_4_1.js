
function isMatch(n) {
    const digits = n.toString().split('').map(x => Number(x));
    let current = -Infinity;

    if(!/(.)\1/.test(n)) {
        return false;
    }

    for(let d of digits) {
        if(d < current) {
            return false;
        }
        current = d;
    }

    return true;
}


let count = 0;


for(let i = 109165; i <= 576723; i++) {
    if(isMatch(i)) {
        count++;
    }
}

console.log(count);