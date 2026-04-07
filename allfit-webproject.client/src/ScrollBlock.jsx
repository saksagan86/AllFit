import { useState } from 'react';
function ScrollBlock({ items }) {
    const [index, setIndex] = useState(0);

    function handleNext() {
        if (index + 1 < items.length) {
            setIndex(index + 1);
        } else {
            setIndex(0)
        }
    }
    function handlePrev() {
        if (index - 1 >= 0) {
            setIndex(index - 1);
        } else {
            setIndex(items.length - 1)
        }
    }

    let item = items[index];
    return (
        <div>
            {item}
            <div class='scroll-bar'>
                <button onClick={handlePrev}>
                    Previous
                </button>
                <p>
                    {index + 1} / {items.length}
                </p>
                <button onClick={handleNext}>
                    Next
                </button>
            </div>
        </div>

  );
}

export default ScrollBlock;