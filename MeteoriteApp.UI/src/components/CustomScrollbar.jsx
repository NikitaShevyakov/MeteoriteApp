import { useState, useEffect, useRef } from 'react';
import { styled } from '@mui/material/styles';

const Track = styled('div')({
  position: 'absolute',
  top: 0,
  right: 0,
  bottom: 0,            
  width: 8,
  background: 'transparent',
  zIndex: 10,
});

const Thumb = styled('div')(({ theme, thumbHeight, thumbTop }) => ({
  position: 'absolute',
  right: 0,
  width: 8,
  height: thumbHeight,
  top: thumbTop,
  background: theme.palette.primary.main,
  borderRadius: 4,
  cursor: 'pointer',
}));

export function CustomScrollbar({ containerRef, rows }) {
  const [thumbHeight, setThumbHeight] = useState(0);
  const [thumbTop, setThumbTop] = useState(0);
  const [isScrollable, setIsScrollable] = useState(false);

  const dragging = useRef(false);
  const dragStartY = useRef(0);
  const startScrollTop = useRef(0);

  useEffect(() => {
    const el = containerRef.current;
    if (!el) return;

    el.scrollTop = 0;

    const calculate = () => {
      const viewH = el.clientHeight;
      const scrollH = el.scrollHeight;
      setIsScrollable(scrollH > viewH);
      const h = Math.max((viewH / scrollH) * viewH, 30);
      setThumbHeight(h);
      setThumbTop((el.scrollTop / scrollH) * viewH);
    };

    calculate();
    el.addEventListener('scroll', calculate);
    window.addEventListener('resize', calculate);
    return () => {
      el.removeEventListener('scroll', calculate);
      window.removeEventListener('resize', calculate);
    };
  }, [containerRef, rows]);

  const onMouseDown = (e) => {
    dragging.current = true;
    dragStartY.current = e.clientY;
    startScrollTop.current = containerRef.current.scrollTop;
    document.addEventListener('mousemove', onMouseMove);
    document.addEventListener('mouseup', onMouseUp);
    e.preventDefault();
  };

  const onMouseMove = (e) => {
    if (!dragging.current) return;
    const el = containerRef.current;
    const deltaY = e.clientY - dragStartY.current;
    const scrollable = el.scrollHeight - el.clientHeight;
    const trackSpace = el.clientHeight - thumbHeight;
    el.scrollTop =
      startScrollTop.current + (deltaY / trackSpace) * scrollable;
  };

  const onMouseUp = () => {
    dragging.current = false;
    document.removeEventListener('mousemove', onMouseMove);
    document.removeEventListener('mouseup', onMouseUp);
  };

  if (!isScrollable) return null;

  return (
    <Track>
      <Thumb
        thumbHeight={thumbHeight}
        thumbTop={thumbTop}
        onMouseDown={onMouseDown}
      />
    </Track>
  );
}
