function showToast(opration, title, message, color, position) {

    //positions=topRight,bottomRight,topLeft,bottomLeft,topCenter,bottomCenter

    a = opration,
        e = "",
        i = position,
        n = title,
        o = message,
        p = color,
        s = "";


    s = {
        rtl: true,
        class: "iziToast-" + a || "",
        title: n,
        message: o,
        animateInside: !1,
        position: i,
        progressBar: !1,
        icon: e,
        timeout: 3200,
        transitionIn: "fadeInLeft",
        transitionOut: "fadeOut",
        transitionInMobile: "fadeIn",
        transitionOutMobile: "fadeOut",
        color: p || "blue",
    };

    iziToast.show(s);
}