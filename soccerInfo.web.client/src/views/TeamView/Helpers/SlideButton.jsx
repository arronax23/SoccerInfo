import LeftSlider from "/sliders/left-slider.svg";
import RightSlider from "/sliders/right-slider.svg";

export default function SlideButton({ swiperRef, isNext }) {

  {
    if (isNext)
      return (
        <div
          className="carousel-button carousel-button-next"
          onClick={() => swiperRef.current?.slideNext()}
        >
          <img style={{ height: "50px" }} src={RightSlider} alt="loading" />
        </div>
      );
    else
      return (
        <div
          className="carousel-button carousel-button-prev"
          onClick={() => swiperRef.current?.slidePrev()}
        >
          <img style={{ height: "50px" }} src={LeftSlider} alt="loading" />
        </div>
      );
  }
}
