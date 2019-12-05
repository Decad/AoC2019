const path1 = 'R995,U671,R852,U741,R347,U539,R324,U865,R839,U885,R924,D983,R865,D823,R457,U124,R807,U941,R900,U718,R896,D795,R714,D129,R465,U470,L625,U200,L707,U552,L447,D305,L351,D571,L346,D38,L609,U581,L98,D707,R535,D332,L23,D630,L66,U833,L699,D445,L981,D81,L627,U273,R226,D51,L177,D806,R459,D950,R627,U462,L382,D847,R335,D573,L902,D581,L375,D288,R26,U922,R710,D159,R481,U907,L852,U926,L905,D140,L581,U908,R158,D955,R349,U708,R196,D13,R628,D862,L899,U50,L56,D89,L506,U65,R664,D243,L701,D887,L552,U665,L674,U813,L433,U87,R951,D970,R914,D705,R79,U328,L107,D86,L307,U550,L872,U224,L595,D600,R442,D426,L139,U528,R680,U35,L951,D275,L78,U113,L509,U821,R150,U668,L981,U102,L632,D864,R636,D597,R385,U322,R464,U249,L286,D138,L993,U329,R874,D849,R6,D632,L751,U235,R817,D495,L152,D528,R872,D91,R973,D399,L14,D544,R20,U54,L793,U90,L756,D36,R668,D221,L286,D681,L901,U312,R290,D874,L155,U863,R35,D177,R900,D865,R250,D810,L448,D648,L358,U308,R986,D562,L112,D858,R77,D880,L12,U702,L987,D662,R771,U6,R643,U845,R54,U987,L994,D878,L934,U805,L85,D760,L775,D578,L557,U544,L522,U495,L678,D68,R615,U700,L415,U597,L964,D858,R504,U805,L392,U140,L721,D215,L842,U929,L30,U64,L748,D136,R274,D605,R863,U460,L354,U78,R705,D298,L456,U117,R308,D186,L707,D367,R824,U965,L162,D19,R950,D582,R911,D436,L165,U506,L186,D906,L69,U412,R810,U13,L350,U314,R192,U963,L143,D937,L685,D574,R434,D937,L365,U646,L741,U703,L66,U959,L103,U799,L480,U340,R981,U96,L675,U662,R536,U15,R171,U382,R396,D431,L922,D662,R365,D921,R915';
const path2 = 'L999,D290,L462,D773,L687,D706,L785,D219,R102,U307,L466,D166,R11,D712,L675,D844,R834,U665,R18,D91,R576,U187,L832,D969,L856,U389,R275,D587,L153,U329,R833,U762,R487,U607,R232,D361,R301,D738,L121,D896,R729,D767,R596,U996,R856,D849,R748,D506,L949,U166,R194,D737,L946,D504,L908,D980,L249,U885,R930,D910,R860,D647,L985,U688,L695,U207,L182,D444,R809,D394,R441,U664,L721,U31,R690,U597,R694,U942,R878,U320,R874,U162,L840,U575,L602,U649,L337,D775,L316,D588,R603,D175,L299,D538,R117,U213,L542,D429,R969,D641,R946,D373,L406,D119,R58,D686,R460,U906,L303,D13,L209,D546,R33,D545,R806,U615,R416,D294,L932,D877,R270,U350,R40,U720,L248,D13,L120,D657,L787,U313,R93,U922,R330,D184,L595,D578,R144,D213,L827,U787,R41,D142,R340,D733,L547,U595,L49,U652,L819,D691,R871,D628,R117,U880,L140,U736,L776,U151,R781,U582,R438,D382,R747,D390,R956,U44,L205,U680,R775,D152,L8,D80,R730,U922,L348,U363,L44,D355,R556,D880,R734,U60,R102,U776,L822,D732,L332,D769,L272,D784,R908,U58,L252,U290,R478,D192,R638,U548,R169,D946,L749,D638,L962,U844,R458,D283,R354,U95,L271,U738,R764,U757,R862,U176,L699,D810,L319,U866,R585,U743,L483,D502,R904,D248,L792,D37,R679,U607,L439,U326,L105,U95,L486,D214,R981,U260,R801,U212,L718,U302,L644,D987,L73,U228,L576,U507,L231,D63,R871,U802,R282,D237,L277,U418,R116,U194,R829,U786,L982,D131,R630,U358,R939,D945,L958,D961,R889,U949,L469,D980,R25,D523,L830,U343,R780,U581,R562,U115,L569,D959,R738,U299,L719,U732,L444,D579,L13,U242,L953,U169,R812,D821,R961,D742,R814,D483,R479,D123,L745,D892,L534';

// const path1 = 'R75,D30,R83,U83,L12,D49,R71,U7,L72';
// const path2 = 'U62,R66,U55,R34,D71,R55,D58,R83';

// const path1 = 'R8,U5,L5,D3';
// const path2 = 'U7,R6,D4,L4';


function walk(path) {

    let location = [0, 0];
    const points = [[0, 0]];

    path.split(',').forEach((step) => {
        const direction = step.substr(0, 1);
        const length = Number(step.substr(1));

        switch (direction) {
            case 'R':
                location = [location[0] + length, location[1]];
                break;
            case 'L':
                location = [location[0] - length, location[1]];
                break;
            case 'D':
                location = [location[0], location[1] - length];
                break;
            case 'U':
                location = [location[0], location[1] + length];
                break;
        }

        points.push(location);
    });

    return points;
}


function findIntercect(hLine, vLine) {
    if (vLine[0][0] >= hLine[0][0] && vLine[0][0] <= hLine[1][0]) {
        if (hLine[0][1] >= vLine[0][1] && hLine[0][1] <= vLine[1][1]) {
            const intercect = [vLine[0][0], hLine[0][1]];
            if (intercect[0] !== 0 && intercect[1] !== 0) {
                return intercect;
            }
        }
    }
    return null;
}


function intercect(line1, line2) {
    const l1minx = Math.min(line1[0][0], line1[1][0]);
    const l1miny = Math.min(line1[0][1], line1[1][1]);
    const l1maxx = Math.max(line1[0][0], line1[1][0]);
    const l1maxy = Math.max(line1[0][1], line1[1][1]);

    const l2minx = Math.min(line2[0][0], line2[1][0]);
    const l2miny = Math.min(line2[0][1], line2[1][1]);
    const l2maxx = Math.max(line2[0][0], line2[1][0]);
    const l2maxy = Math.max(line2[0][1], line2[1][1]);

    const l1h = l1miny == l1maxy;
    const l2h = l2miny == l2maxy;

    if (l1h == l2h) return null;

    if (l1h) {
        return findIntercect([[l1minx, l1miny], [l1maxx, l1miny]], [[l2minx, l2miny], [l2minx, l2maxy]]);
    }

    return findIntercect([[l2minx, l2miny], [l2maxx, l2miny]], [[l1minx, l1miny], [l1minx, l1maxy]]);
}

const points1 = walk(path1);
const points2 = walk(path2);
const intercections = [];

function solve() {

    for (let i = 0; i < points1.length - 1; i++) {
        let line1 = [points1[i], points1[i + 1]];
        for (let j = 0; j < points2.length - 1; j++) {
            let line2 = [points2[j], points2[j + 1]];
            const intercectsAt = intercect(line1, line2);

            if (intercectsAt) {
                intercections.push(intercectsAt)
            }
        }
    }

    const closest = intercections.reduce((current, point) => {
        const dist = Math.abs(point[0]) + Math.abs(point[1]);
        if (dist < current)
            return dist;

        return current;
    }, Infinity);

    console.log(closest);
}

solve();
