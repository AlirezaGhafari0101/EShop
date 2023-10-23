
const discountCalc = (price, discountPercentage) => {

    const result = (price * (100 - discountPercentage)) / 100
    return result
}

const profitCalc = (orginalPrice, discountedPrice) => {
    return orginalPrice - discountedPrice
}



