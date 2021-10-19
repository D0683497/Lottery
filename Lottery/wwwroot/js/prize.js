class Prize {
    constructor(id, last, total) {
        this.id = id;
        this.last = last;
        this.total = total;
    }
}

class PrizeList {

    constructor() {
        let prizes = [];
        document.querySelectorAll('.media').forEach(function (e) {
            let id = e.id.split('prize-')[1];
            let progress = e.querySelector(`#prize-count-${id}`);
            let last = progress.value;
            let total = progress.max;
            prizes.push(new Prize(id, last, total));
        });
        prizes.forEach(function (prize, index) {
            if (prize.last === 0) {
                prizes.splice(index, 1);
            }
        });
        this.prizes = prizes;
    }
    
    get top() {
        return this.prizes[0];
    }
    
    get topId() {
        return this.prizes[0].id;
    }

    active(id) {
        let image = document.querySelector(`#prize-image-${id}`);
        image.classList.remove('is-48x48');
        image.classList.add('is-64x64');
        let name = document.querySelector(`#prize-name-${id}`);
        name.classList.remove('subtitle', 'is-6', 'has-text-grey');
        name.classList.add('title', 'is-5', 'has-text-black');
        let progress = document.querySelector(`#prize-count-${id}`);
        progress.classList.remove('is-info', 'is-small');
        progress.classList.add('is-primary', 'is-medium');
    }

    inActive(id) {
        let image = document.querySelector(`#prize-image-${id}`);
        image.classList.remove('is-64x64');
        image.classList.add('is-48x48');
        let name = document.querySelector(`#prize-name-${id}`);
        name.classList.remove('title', 'is-5', 'has-text-black');
        name.classList.add('subtitle', 'is-6', 'has-text-grey');
        let progress = document.querySelector(`#prize-count-${id}`);
        progress.classList.remove('is-primary', 'is-medium');
        progress.classList.add('is-info', 'is-small');
    }

    reduce() {
        let prize = this.top;
        prize.last--;
        document.querySelector(`#prize-count-${prize.id}`).value = prize.last;
        if (prize.last === 0) {
            this.inActive(prize.id);
            this.prizes.splice(0, 1);
            this.active(this.topId);
        }
    }
}

export { PrizeList };