//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Post Processing")]
    [Tooltip("Controller Set for URP Post Effects (Volume)")]

    public class URPPostProcessingController : FsmStateActionAdvanced
    {
        //Enums
        #region ENUMS
        public enum volumeOverrides
        {
            None,
            Bloom,
            ChannelMixer,
            ChromaticAberration,
            ColorAdjustments,
            ColorLookup,
            DepthOfField,
            FilmGrain,
            LensDistortion,
            LiftGammaGain,
            MotionBlur,
            PaniniProjection,
            ShadowsMidtonesHighlights,
            SplitToning,
            Vignette,
            WhiteBalance
        }
        #endregion

        //Show & Hide Switches
        #region SWITCH VARIABLES
        public bool HideBloom() { if (effect == volumeOverrides.Bloom) return false; else return true; }
        public bool HideChannelMixer() { if (effect == volumeOverrides.ChannelMixer) return false; else return true; }
        public bool HideChromaticAberration() { if (effect == volumeOverrides.ChromaticAberration) return false; else return true; }
        public bool HideColorAdjustments() { if (effect == volumeOverrides.ColorAdjustments) return false; else return true; }
        public bool HideColorLookup() { if (effect == volumeOverrides.ColorLookup) return false; else return true; }
        public bool HideDepthOfField() { if (effect == volumeOverrides.DepthOfField) return false; else return true; }
        public bool HideFilmGrain() { if (effect == volumeOverrides.FilmGrain) return false; else return true; }
        public bool HideLensDistortion() { if (effect == volumeOverrides.LensDistortion) return false; else return true; }
        public bool HideLiftGammaGain() { if (effect == volumeOverrides.LiftGammaGain) return false; else return true; }
        public bool HideMotionBlur() { if (effect == volumeOverrides.MotionBlur) return false; else return true; }
        public bool HidePaniniProjection() { if (effect == volumeOverrides.PaniniProjection) return false; else return true; }
        public bool HideShadowsMidtonesHighlights() { if (effect == volumeOverrides.ShadowsMidtonesHighlights) return false; else return true; }
        public bool HideSplitToning() { if (effect == volumeOverrides.SplitToning) return false; else return true; }
        public bool HideVignette() { if (effect == volumeOverrides.Vignette) return false; else return true; }
        public bool HideWhiteBalance() { if (effect == volumeOverrides.WhiteBalance) return false; else return true; }
        #endregion

        //Variables
        #region SETUP VARIABLES
        [ActionSection("Setup")]

        [RequiredField]
        [Title("Volume")]
        [CheckForComponent(typeof(Volume))]
        [Tooltip("Will not be changed during Runtime")]
        public FsmOwnerDefault volumeOwner;

        [Title("Global Mode")]
        [Tooltip("Will not be changed during Runtime")]
        public FsmBool global;

        [Title("Weight")]
        [HasFloatSlider(0, 1)]
        public FsmFloat volumeWeight;
        [Title("Priority")]
        public FsmFloat volumePriority;

        [ActionSection("Effects")]
        [Title("Effects")]
        public volumeOverrides effect;
        #endregion

        #region BLOOM VARIABLES
        [HideIf("HideBloom")]
        [ActionSection("Bloom")]
        [Title("Threshold")]
        public FsmFloat bloomThreshold;

        [Title("Intensity")]
        [HideIf("HideBloom")]
        public FsmFloat bloomIntensity;

        [Title("Scatter")]
        [HideIf("HideBloom")]
        [HasFloatSlider(0, 1)]
        public FsmFloat bloomScatter;

        [Title("Tint")]
        [HideIf("HideBloom")]
        public FsmColor bloomTint;

        [Title("Clamp")]
        [HideIf("HideBloom")]
        public FsmFloat bloomClamp;

        [Title("HQ Filtering")]
        [HideIf("HideBloom")]
        public FsmBool bloomHQFiltering;

        [Title("HQ Filtering")]
        [HideIf("HideBloom")]
        [HasIntSlider(0, 16)]
        public FsmInt bloomSkipIterations;

        [ActionSection("Effects")]
        [Title("Dirt Texture")]
        [HideIf("HideBloom")]
        public FsmTexture bloomDirtTexture;

        [Title("Dirt Intensity")]
        [HideIf("HideBloom")]
        public FsmFloat bloomDirtIntensity;
        #endregion

        #region CHANNEL MIXER VARIABLES
        [HideIf("HideChannelMixer")]
        [ActionSection("Channel Mixer - RED")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat redRed;
        [HideIf("HideChannelMixer")]

        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat redGreen;
        [HideIf("HideChannelMixer")]

        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat redBlue;

        [HideIf("HideChannelMixer")]
        [ActionSection("Channel Mixer - GREEN")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat greenRed;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat greenGreen;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat greendBlue;

        [HideIf("HideChannelMixer")]
        [ActionSection("Channel Mixer - BLUE")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat blueRed;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat blueGreen;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat blueBlue;

        #endregion

        #region CHROMATIC ABERRATION VARIABLES
        [HideIf("HideColorAdjustments")]
        [ActionSection("Color Adjustments")]
        [Title("Post Exposure")]
        public FsmFloat colorAdjustmentsExposure;

        [HideIf("HideColorAdjustments")]
        [HasFloatSlider(-100, 100)]
        [Title("Contrast")]
        public FsmFloat colorAdjustmentsContrast;

        [HideIf("HideColorAdjustments")]
        [Title("Color Filter")]
        public FsmColor colorAdjustmentsFilter;

        [HideIf("HideColorAdjustments")]
        [HasFloatSlider(-180, 180)]
        [Title("Hue Shift")]
        public FsmFloat colorAdjustmentsHueShift;

        [HideIf("HideColorAdjustments")]
        [HasFloatSlider(-100, 100)]
        [Title("Saturation")]
        public FsmFloat colorAdjustmentsSaturation;

        #endregion

        #region COLOR ADJUSTMENTS VARIABLES
        [HideIf("HideChromaticAberration")]
        [ActionSection("Channel Mixer")]
        [HasFloatSlider(0, 1)]
        [Title("Intensity")]
        public FsmFloat chromaticAberrationIntensity;

        #endregion

        #region COLOR LOOKUP VARIABLES

        [HideIf("HideColorLookup")]
        [ActionSection("Color Lookup")]
        [Title("Lookup Texture")]
        public FsmTexture colorLookuplookupTexture;

        [HideIf("HideColorLookup")]
        [HasFloatSlider(0, 1)]
        [Title("Contribution")]
        public FsmFloat colorLookupContribution;

        #endregion

        #region DEPTH OF FIELD VARIABLES

        [HideIf("HideDepthOfField")]
        [ActionSection("Depth of Field")]
        [Title("Mode")]
        public DepthOfFieldMode depthMode;

        public bool HideDepthModeGaussian() { if (effect == volumeOverrides.DepthOfField && depthMode == DepthOfFieldMode.Gaussian) return false; else return true; }

        [HideIf("HideDepthModeGaussian")]
        [ActionSection("Gaussian")]
        [Title("Start")]
        public FsmFloat gaussianStart;

        [HideIf("HideDepthModeGaussian")]
        [Title("Start")]
        public FsmFloat gaussianEnd;

        [HideIf("HideDepthModeGaussian")]
        [Title("Max Radius")]
        [HasFloatSlider(0.5f, 1.5f)]
        public FsmFloat gaussianMaxRadius;

        [HideIf("HideDepthModeGaussian")]
        [Title("HQ Sampling")]
        public FsmBool gaussianHQSampling;

        public bool HideDepthModeBokeh() { if (effect == volumeOverrides.DepthOfField && depthMode == DepthOfFieldMode.Bokeh) return false; else return true; }

        [HideIf("HideDepthModeBokeh")]
        [Title("Focus Distance")]
        public FsmFloat bokehFocusDistance;

        [HideIf("HideDepthModeBokeh")]
        [Title("Focal Lenght")]
        [HasIntSlider(1, 300)]
        public FsmInt bokehFocalLenght;

        [HideIf("HideDepthModeBokeh")]
        [Title("Aperture")]
        [HasFloatSlider(1, 32)]
        public FsmFloat bokehAperture;

        [HideIf("HideDepthModeBokeh")]
        [Title("Blade Count")]
        [HasIntSlider(3, 9)]
        public FsmInt bokehBladeCount;

        [HideIf("HideDepthModeBokeh")]
        [Title("Blade Curvature")]
        [HasFloatSlider(0, 1)]
        public FsmFloat bokehBladeCurvature;

        [HideIf("HideDepthModeBokeh")]
        [Title("Blade Rotation")]
        [HasIntSlider(-180, 180)]
        public FsmInt bokehBladeRotation;
        #endregion

        #region FILM GRAIN VARIABLES

        [HideIf("HideFilmGrain")]
        [ActionSection("Film Grain")]
        [Title("Type")]
        public FilmGrainLookup filmGrainType;

        [HideIf("HideFilmGrain")]
        [Title("Intensity")]
        [HasFloatSlider(0, 1)]
        public FsmFloat filmGrainIntensity;

        [HideIf("HideFilmGrain")]
        [Title("Response")]
        [HasFloatSlider(0, 1)]
        public FsmFloat filmGrainResponse;

        public bool HideFilmGrainCustom() { if (effect == volumeOverrides.FilmGrain && filmGrainType == FilmGrainLookup.Custom) return false; else return true; }
        [HideIf("HideFilmGrainCustom")]
        [Title("Texture")]
        public FsmTexture filmGrainTexture;

        #endregion

        #region LENS DISTORTION VARIABLES
        [HideIf("HideLensDistortion")]
        [ActionSection("Lens Distortion")]
        [Title("Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lensDistortionIntensity;

        [Title("X Multiplier")]
        [HideIf("HideLensDistortion")]
        [HasFloatSlider(0, 1)]
        public FsmFloat lensDistortionXMult;

        [Title("Y Multiplier")]
        [HideIf("HideLensDistortion")]
        [HasFloatSlider(0, 1)]
        public FsmFloat lensDistortionYMult;

        [Title("Center")]
        [HideIf("HideLensDistortion")]
        public FsmVector2 lensDistortionCenter;

        [Title("Scale")]
        [HideIf("HideLensDistortion")]
        [HasFloatSlider(0.01f, 5)]
        public FsmFloat lensDistortionScale;
        #endregion

        #region LIFT GAMMA GAIN VARIABLES
        [HideIf("HideLiftGammaGain")]
        [ActionSection("Lift Gamma Gain")]
        [Title("Lift")]
        public FsmColor lggLift;

        [HideIf("HideLiftGammaGain")]
        [Title("Lift Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lggLiftIntensity;

        [HideIf("HideLiftGammaGain")]
        [Title("Gamma")]
        public FsmColor lggGamma;

        [HideIf("HideLiftGammaGain")]
        [Title("Gamm Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lggGammaIntensity;

        [HideIf("HideLiftGammaGain")]
        [Title("Gain")]
        public FsmColor lggGain;

        [HideIf("HideLiftGammaGain")]
        [Title("Gain Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lggGainIntensity;
        #endregion

        #region MOTION BLUR VARIABLES

        [HideIf("HideMotionBlur")]
        [ActionSection("Motion Blur")]
        [Title("Mode")]
        public MotionBlurMode motionBlurMode;

        [HideIf("HideMotionBlur")]
        [Title("Quality")]
        public MotionBlurQuality motionBlurQuality;

        [HideIf("HideMotionBlur")]
        [Title("Intensity")]
        [HasFloatSlider(0, 1)]
        public FsmFloat motionBlurIntensity;

        [HideIf("HideMotionBlur")]
        [Title("Clamp")]
        [HasFloatSlider(0, 0.2f)]
        public FsmFloat motionBlurClamp;
        #endregion

        #region PANINI PROJECTION VARIABLES
        [HideIf("HidePaniniProjection")]
        [ActionSection("Panini Projection")]
        [Title("Distance")]
        [HasFloatSlider(0, 1)]
        public FsmFloat paniniDistance;

        [HideIf("HidePaniniProjection")]
        [Title("Crop to Fit")]
        [HasFloatSlider(0, 1)]
        public FsmFloat paniniCrop;
        #endregion

        #region SHADOWS MIDTONES HIGHLIGHTS VARIABLES
        [HideIf("HideShadowsMidtonesHighlights")]
        [ActionSection("Lift Gamma Gain")]
        [Title("Shadows")]
        public FsmColor smhShadows;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Shadows Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat smhShadowsIntensity;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Midtones")]
        public FsmColor smhMidtones;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Midtones Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat smhMidtonesIntensity;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Highlights")]
        public FsmColor smhHighlights;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Highlights Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat smhHighlightsIntensity;

        [HideIf("HideShadowsMidtonesHighlights")]
        [ActionSection("Shadow Limits")]
        [Title("Start")]
        public FsmFloat smhShadowLimitStart;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("End")]
        public FsmFloat smhShadowLimitEnd;

        [HideIf("HideShadowsMidtonesHighlights")]
        [ActionSection("Highlights Limits")]
        [Title("Start")]
        public FsmFloat smhHighlightLimitStart;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("End")]
        public FsmFloat smhHighlightLimitEnd;
        #endregion

        #region SPLIT TONING VARIABLES
        [HideIf("HideSplitToning")]
        [ActionSection("Split Toning")]
        [Title("Shadows")]
        public FsmColor splitToningShadows;

        [HideIf("HideSplitToning")]
        [Title("Highlights")]
        public FsmColor splitToningHighlights;

        [HideIf("HideSplitToning")]
        [Title("Highlights")]
        [HasIntSlider(-100, 100)]
        public FsmInt splitToningBalance;
        #endregion

        #region VIGNETTE VARIABLES
        [HideIf("HideVignette")]
        [ActionSection("Vignette")]
        [Title("Color")]
        public FsmColor vignetteColor;

        [HideIf("HideVignette")]
        [Title("Center")]
        public FsmVector2 vignetteCenter;

        [HideIf("HideVignette")]
        [Title("Intensity")]
        [HasFloatSlider(0, 1)]
        public FsmFloat vignetteIntensity;

        [HideIf("HideVignette")]
        [Title("Smoothness")]
        [HasFloatSlider(0, 1)]
        public FsmFloat vignetteSmoothness;

        [HideIf("HideVignette")]
        [Title("Rounded")]
        public FsmBool vignetteRounded;

        #endregion

        #region WHITE BALANCE VARIABLES
        [HideIf("HideWhiteBalance")]
        [ActionSection("White Balance")]
        [Title("Temperature")]
        [HasIntSlider(-100, 100)]
        public FsmInt wbTemperatere;

        [HideIf("HideWhiteBalance")]
        [Title("Tint")]
        [HasIntSlider(-100, 100)]
        public FsmInt wbTint;
        #endregion

        #region PRIVATE VARIABLES
        private GameObject volumeObj; 
        private Volume volume;
        private VolumeProfile volumeProfile;
        #endregion

        //Main

        #region ERRORCHECK()
        public override string ErrorCheck()
        {
            volumeObj = Fsm.GetOwnerDefaultTarget(volumeOwner);

            if (volumeObj == null)
                return ("A GameObject with a Volume Component is Required. (Current GameObject Value is Null)");
            else if (!volumeObj.TryGetComponent(out Volume vol))
                return ("A GameObject with a Volume Component is Required (Couldn't Find a Volume Component on the GameObject.");

            volume = volumeObj.GetComponent<Volume>();

            #region OVERRIDE CHECK
            if (effect == volumeOverrides.Bloom)
                if (!volume.sharedProfile.TryGet<Bloom>(out var bloom))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.ChannelMixer)
                if (!volume.sharedProfile.TryGet<ChannelMixer>(out var channelMixer))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.ChromaticAberration)
                if (!volume.sharedProfile.TryGet<ChromaticAberration>(out var chromaticAberration))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.ColorAdjustments)
                if (!volume.sharedProfile.TryGet<ColorAdjustments>(out var colorAdjustments))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.ColorLookup)
                if (!volume.sharedProfile.TryGet<ColorLookup>(out var colorLookup))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.DepthOfField)
                if (!volume.sharedProfile.TryGet<DepthOfField>(out var depthOfField))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.FilmGrain)
                if (!volume.sharedProfile.TryGet<FilmGrain>(out var filmGrain))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.LensDistortion)
                if (!volume.sharedProfile.TryGet<LensDistortion>(out var lensDistortion))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.LiftGammaGain)
                if (!volume.sharedProfile.TryGet<LiftGammaGain>(out var liftGammaGain))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.MotionBlur)
                if (!volume.sharedProfile.TryGet<MotionBlur>(out var motionBlur))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.PaniniProjection)
                if (!volume.sharedProfile.TryGet<PaniniProjection>(out var paniniProjection))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.ShadowsMidtonesHighlights)
                if (!volume.sharedProfile.TryGet<ShadowsMidtonesHighlights>(out var shadowsMidtonesHighlights))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.SplitToning)
                if (!volume.sharedProfile.TryGet<SplitToning>(out var splitToning))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.Vignette)
                if (!volume.sharedProfile.TryGet<Vignette>(out var vignette))
                     return ("No " + effect + " Override found on Volume."); 
            if (effect == volumeOverrides.WhiteBalance)
                if (!volume.sharedProfile.TryGet<WhiteBalance>(out var whiteBalance))
                     return ("No " + effect + " Override found on Volume."); 
            #endregion
            
            return "";
        }
        #endregion

        #region RESET()
        public override void Reset()
        {

            #region SETUP RESET
            volume = null;
            volumeProfile = null;
            global = true;
            volumeWeight = 1;
            volumePriority = 0;
            #endregion

            #region BLOOM RESET
            bloomThreshold = 0.9f;
            bloomIntensity = 0;
            bloomScatter = 0.7f;
            bloomTint = new Color (1, 1, 1, 1);
            bloomClamp = 65472f;
            bloomHQFiltering = false;
            bloomSkipIterations = 1;
            bloomDirtTexture = new FsmTexture { UseVariable = true };
            bloomDirtIntensity = 0f;
            #endregion

            #region CHANNEL MIXER RESET
            redRed = 100f;
            redGreen = 0f;
            redBlue = 0f;
            greenRed = 0;
            greenGreen = 100;
            greendBlue = 0;
            blueRed = 0;
            blueGreen = 0;
            blueBlue = 100;
            #endregion

            #region CHROMATIC ABERRATION RESET
            chromaticAberrationIntensity = 1;
            #endregion

            #region COLOR ADJUSTMENTS RESET
            colorAdjustmentsExposure = 0;
            colorAdjustmentsContrast = 0;
            colorAdjustmentsFilter = new Color(1, 1, 1, 1);
            colorAdjustmentsHueShift = 0;
            colorAdjustmentsSaturation = 0;
            #endregion

            #region COLOR LOOKUP RESET
            colorLookuplookupTexture = new FsmTexture { UseVariable = true };
            colorLookupContribution = 1f;
            #endregion

            #region DEPTH OF FIELD RESET
            depthMode = DepthOfFieldMode.Off;
            gaussianStart = 10;
            gaussianEnd = 30;
            gaussianMaxRadius = 1;
            gaussianHQSampling = false;

            bokehFocusDistance = 10;
            bokehFocalLenght = 50;
            bokehAperture = 5.6f;
            bokehBladeCount = 5;
            bokehBladeCurvature = 1;
            bokehBladeRotation = 0;
            #endregion

            #region FILM GRAIN RESET
            filmGrainType = FilmGrainLookup.Thin1;
            filmGrainIntensity = 0;
            filmGrainResponse = 0.8f;
            filmGrainTexture = new FsmTexture { UseVariable = true };
            #endregion

            #region LENS DISTORTION RESET
            lensDistortionIntensity = 0;
            lensDistortionXMult = 1;
            lensDistortionYMult = 1;
            lensDistortionCenter = new Vector2(0.5f, 0.5f);
            lensDistortionScale = 1;
            #endregion

            #region LIFT GAMMA GAIN RESET
            lggLift = new Color(1, 1, 1, 0);
            lggLiftIntensity = 0;
            lggGamma = new Color(1, 1, 1, 0);
            lggGammaIntensity = 0;
            lggGain = new Color(1, 1, 1, 0);
            lggGainIntensity = 0;
            #endregion

            #region MOTION BLUR RESET
            motionBlurMode = MotionBlurMode.CameraAndObjects;
            motionBlurQuality = MotionBlurQuality.Low;
            motionBlurIntensity = 0f;
            motionBlurClamp = 0.05f;
            #endregion

            #region PANINI PROJECTION RESET
            paniniDistance = 0;
            paniniCrop = 1;
            #endregion

            #region SHADOWS MIDTONES HIGHLIGHTS RESET
            smhShadows = new Color(1, 1, 1, 0);
            smhShadowsIntensity = 0;
            smhMidtones = new Color(1, 1, 1, 0);
            smhMidtonesIntensity = 0;
            smhHighlights = new Color(1, 1, 1, 0);
            smhHighlightsIntensity = 0;
            smhShadowLimitStart = 0;
            smhShadowLimitEnd = 0.3f;
            smhHighlightLimitStart = 0.55f;
            smhHighlightLimitEnd = 1;
            #endregion

            #region SPLIT TONING RESET
            splitToningShadows = new Color(0.5f, 0.5f, 0.5f, 1);
            splitToningHighlights = new Color(0.5f, 0.5f, 0.5f, 1);
            splitToningBalance = 0;
            #endregion

            #region VIGNETTE RESET
            vignetteColor = new Color(0, 0, 0, 1);
            vignetteCenter = new Vector2(0.5f, 0.5f);
            vignetteIntensity = 0;
            vignetteSmoothness = 0.2f;
            vignetteRounded = false;
            #endregion

            #region WHITE BALANCE RESET
            wbTemperatere = 0;
            wbTint = 0;
            #endregion

        }
        #endregion

        #region ENTER() and UPDATE()
        public override void OnEnter()
        {
            volumeObj = Fsm.GetOwnerDefaultTarget(volumeOwner);

            #region RUNTIME ERROR CHECK LOGGING & BREAK

            if (volumeObj == null)
            {
                LogWarning("There is no GameObject with Volume component" + " @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
            if (!volumeObj.TryGetComponent(out Volume vol))
            {
                LogWarning ("There is no Volume component on [" + volumeObj.name + "] @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
            if (volumeObj.GetComponent<Volume>().sharedProfile == null)
            {
                LogWarning("There is no profile in the selected Volume ["  + volumeObj.name + "] @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
            
            #endregion

            volumeProfile = volumeObj.GetComponent<Volume>().sharedProfile;
            volume = volumeObj.GetComponent<Volume>();

            volume.isGlobal = global.Value;

            Action();

            if (!everyFrame) Finish();
        }

        //On Enter & Update
        public override void OnActionUpdate()
        {
            Action();
        }
        #endregion

        #region ACTION()
        public void Action()
        {
            volumeProfile = volumeObj.GetComponent<Volume>().sharedProfile;
            volume = volumeObj.GetComponent<Volume>();

            volume.weight = volumeWeight.Value;
            volume.priority = volumePriority.Value;

            if (effect == volumeOverrides.Bloom) Bloom();
            if (effect == volumeOverrides.ChannelMixer) ChannelMixer();
            if (effect == volumeOverrides.ChromaticAberration) ChromaticAberration();
            if (effect == volumeOverrides.ColorAdjustments) ColorAdjustments();
            if (effect == volumeOverrides.ColorLookup) ColorLookup();
            if (effect == volumeOverrides.DepthOfField) DepthOfField();
            if (effect == volumeOverrides.FilmGrain) FilmGrain();
            if (effect == volumeOverrides.LensDistortion) LensDistortion();
            if (effect == volumeOverrides.LiftGammaGain) LiftGammaGain();
            if (effect == volumeOverrides.MotionBlur) MotionBlur();
            if (effect == volumeOverrides.PaniniProjection) PaniniProjection();
            if (effect == volumeOverrides.ShadowsMidtonesHighlights) ShadowsMidtonesHighlights();
            if (effect == volumeOverrides.SplitToning) SplitToning();
            if (effect == volumeOverrides.Vignette) Vignette();
            if (effect == volumeOverrides.WhiteBalance) WhiteBalance();
        }
        #endregion

        //Effects

        #region BLOOM()
        public void Bloom()
        {
            if (!volume.sharedProfile.TryGet<Bloom>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
                
            volumeProfile.TryGet<Bloom>(out var bloom);

            bloom.SetAllOverridesTo(true);

            bloom.threshold.value = bloomThreshold.Value;
            bloom.intensity.value = bloomIntensity.Value;
            bloom.scatter.value = bloomScatter.Value;
            bloom.tint.value = bloomTint.Value;
            bloom.clamp.value = bloomClamp.Value;
            bloom.highQualityFiltering.value = bloomHQFiltering.Value;
            bloom.skipIterations.value = bloomSkipIterations.Value;
            bloom.dirtTexture.value = bloomDirtTexture.Value;
            bloom.dirtIntensity.value = bloomDirtIntensity.Value;
        }
        #endregion

        #region CHANNEL MIXER()
        public void ChannelMixer()
        {
            if (!volume.sharedProfile.TryGet<ChannelMixer>(out var overridetest))
            {
                LogWarning("No " + effect + " found on volume @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ChannelMixer>(out var mixer);

            mixer.SetAllOverridesTo(true);

            mixer.redOutRedIn.value = redRed.Value;
            mixer.redOutGreenIn.value = redGreen.Value;
            mixer.redOutBlueIn.value = redBlue.Value;

            mixer.greenOutRedIn.value = greenRed.Value;
            mixer.greenOutGreenIn.value = greenGreen.Value;
            mixer.greenOutBlueIn.value = greendBlue.Value;

            mixer.blueOutRedIn.value = blueRed.Value;
            mixer.blueOutGreenIn.value = blueGreen.Value;
            mixer.blueOutBlueIn.value = blueBlue.Value;

        }
        #endregion

        #region CHORMATIC ABERRATION()
        public void ChromaticAberration()
        {
            if (!volume.sharedProfile.TryGet<ChromaticAberration>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ChromaticAberration>(out var chromaticAberration);

            chromaticAberration.SetAllOverridesTo(true);

            chromaticAberration.intensity.value = chromaticAberrationIntensity.Value;
        }
        #endregion

        #region COLOR ADJUSTMENTS()
        public void ColorAdjustments()
        {
            if (!volume.sharedProfile.TryGet<ColorAdjustments>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ColorAdjustments>(out var colorAdjustments);

            colorAdjustments.SetAllOverridesTo(true);

            colorAdjustments.postExposure.value = colorAdjustmentsExposure.Value;
            colorAdjustments.contrast.value = colorAdjustmentsContrast.Value;
            colorAdjustments.colorFilter.value = colorAdjustmentsFilter.Value;
            colorAdjustments.hueShift.value = colorAdjustmentsHueShift.Value;
            colorAdjustments.saturation.value = colorAdjustmentsSaturation.Value;
        }
        #endregion

        #region COLOR LOOKUP()
        public void ColorLookup()
        {
            if (!volume.sharedProfile.TryGet<ColorLookup>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ColorLookup>(out var colorLookup);

            colorLookup.SetAllOverridesTo(true);

            colorLookup.texture.value = colorLookuplookupTexture.Value;
            colorLookup.contribution.value = colorLookupContribution.Value;


        }
        #endregion

        #region DEPTH OF FIELD()
        public void DepthOfField()
        {
            if (!volume.sharedProfile.TryGet<DepthOfField>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<DepthOfField>(out var depthOfField);

            depthOfField.SetAllOverridesTo(true);

            depthOfField.mode.value = depthMode;

            if (depthMode == DepthOfFieldMode.Gaussian)
            {
                depthOfField.gaussianStart.value = gaussianStart.Value;
                depthOfField.gaussianEnd.value = gaussianEnd.Value;
                depthOfField.gaussianMaxRadius.value = gaussianMaxRadius.Value;
                depthOfField.highQualitySampling.value = gaussianHQSampling.Value;
            }
            else if (depthMode == DepthOfFieldMode.Bokeh)
            {
                depthOfField.focusDistance.value = bokehFocusDistance.Value;
                depthOfField.focalLength.value = bokehFocalLenght.Value;
                depthOfField.aperture.value = bokehAperture.Value;
                depthOfField.bladeCount.value = bokehBladeCount.Value;
                depthOfField.bladeCurvature.value = bokehBladeCurvature.Value;
                depthOfField.bladeRotation.value = bokehBladeRotation.Value;
            }                
            
            
        }
        #endregion

        #region FILM GRAIN()
        public void FilmGrain()
        {
            if (!volume.sharedProfile.TryGet<FilmGrain>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<FilmGrain>(out var filmGrain);

            filmGrain.SetAllOverridesTo(true);

            filmGrain.type.value = filmGrainType;

            filmGrain.intensity.value = filmGrainIntensity.Value;
            filmGrain.response.value = filmGrainResponse.Value;
            if (filmGrainType == FilmGrainLookup.Custom)
                filmGrain.texture.value = filmGrainTexture.Value;            
        }
        #endregion

        #region LENS DISTORTION()
        public void LensDistortion()
        {
            if (!volume.sharedProfile.TryGet<LensDistortion>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<LensDistortion>(out var lensDistortion);

            lensDistortion.SetAllOverridesTo(true);

            lensDistortion.intensity.value = lensDistortionIntensity.Value;
            lensDistortion.xMultiplier.value = lensDistortionXMult.Value;
            lensDistortion.yMultiplier.value = lensDistortionYMult.Value;
            lensDistortion.center.value = lensDistortionCenter.Value;
            lensDistortion.scale.value = lensDistortionScale.Value;
        }
        #endregion

        #region LIFT GAMMA GAIN()
        public void LiftGammaGain()
        {
            if (!volume.sharedProfile.TryGet<LiftGammaGain>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<LiftGammaGain>(out var lgg);

            lgg.SetAllOverridesTo(true);

            Vector4 lggLiftV4 = new Color(lggLift.Value.r, lggLift.Value.g, lggLift.Value.b, lggLiftIntensity.Value);
            Vector4 lggGammaV4 = new Color(lggGamma.Value.r, lggGamma.Value.g, lggGamma.Value.b, lggGammaIntensity.Value);
            Vector4 lggGainV4 = new Color(lggGain.Value.r, lggGain.Value.g, lggGain.Value.b, lggGainIntensity.Value);

            lgg.lift.value = lggLiftV4;
            lgg.gamma.value = lggGammaV4;
            lgg.gain.value = lggGainV4;
        }
        #endregion

        #region MOTION BLUR()
        public void MotionBlur()
        {
            if (!volume.sharedProfile.TryGet<MotionBlur>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<MotionBlur>(out var motionBlur);

            motionBlur.SetAllOverridesTo(true);

            motionBlur.mode.value = motionBlurMode;
            motionBlur.quality.value = motionBlurQuality;
            motionBlur.intensity.value = motionBlurIntensity.Value;
            motionBlur.clamp.value = motionBlurClamp.Value;
        }
        #endregion

        #region PANINI PROJECTION()
        public void PaniniProjection()
        {
            if (!volume.sharedProfile.TryGet<PaniniProjection>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<PaniniProjection>(out var panini);

            panini.SetAllOverridesTo(true);

            panini.distance.value = paniniDistance.Value;
            panini.cropToFit.value = paniniCrop.Value;
        }
        #endregion

        #region SHADOWS MIDTONES HIGHLIGHTS()
        public void ShadowsMidtonesHighlights()
        {
            if (!volume.sharedProfile.TryGet<ShadowsMidtonesHighlights>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ShadowsMidtonesHighlights>(out var smh);

            smh.SetAllOverridesTo(true);

            Vector4 smhShadowsV4 = new Color(smhShadows.Value.r, smhShadows.Value.g, smhShadows.Value.b, smhShadowsIntensity.Value);
            Vector4 smhMidtonesV4 = new Color(smhMidtones.Value.r, smhMidtones.Value.g, smhMidtones.Value.b, smhMidtonesIntensity.Value);
            Vector4 smhHighlightsV4 = new Color(smhHighlights.Value.r, smhHighlights.Value.g, smhHighlights.Value.b, smhHighlightsIntensity.Value);

            smh.shadows.value = smhShadowsV4;
            smh.midtones.value = smhMidtonesV4;
            smh.highlights.value = smhHighlightsV4;

            smh.shadowsStart.value = smhShadowLimitStart.Value;
            if (smhShadowLimitStart.Value < 0) smhShadowLimitStart.Value = 0;
            smh.shadowsEnd.value = smhShadowLimitEnd.Value;
            if (smhShadowLimitEnd.Value < 0) smhShadowLimitEnd.Value = 0;
            if (smhShadowLimitStart.Value > smhShadowLimitEnd.Value) smhShadowLimitStart.Value = smhShadowLimitEnd.Value;

            smh.highlightsStart.value = smhHighlightLimitStart.Value;
            if (smhHighlightLimitStart.Value < 0) smhHighlightLimitStart.Value = 0;
            smh.highlightsEnd.value = smhHighlightLimitEnd.Value;
            if (smhHighlightLimitEnd.Value < 0) smhHighlightLimitEnd.Value = 0;
            if (smhHighlightLimitStart.Value > smhHighlightLimitEnd.Value) smhHighlightLimitStart.Value = smhHighlightLimitEnd.Value;
        }
        #endregion

        #region SPLIT TONING()
        public void SplitToning()
        {
            if (!volume.sharedProfile.TryGet<SplitToning>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<SplitToning>(out var splitToning);

            splitToning.SetAllOverridesTo(true);

            splitToning.shadows.value = splitToningShadows.Value;
            splitToning.highlights.value = splitToningHighlights.Value;
            splitToning.balance.value = splitToningBalance.Value;

        }
        #endregion

        #region VIGNETTE()
        public void Vignette()
        {
            if (!volume.sharedProfile.TryGet<Vignette>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<Vignette>(out var vignette);

            vignette.SetAllOverridesTo(true);

            vignette.color.value = vignetteColor.Value;
            vignette.center.value = vignetteCenter.Value;
            vignette.intensity.value = vignetteIntensity.Value;
            vignette.smoothness.value = vignetteSmoothness.Value;
            vignette.rounded.value = vignetteRounded.Value;
        }
        #endregion

        #region WHITE BALANCE()
        public void WhiteBalance()
        {
            if (!volume.sharedProfile.TryGet<WhiteBalance>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<WhiteBalance>(out var wb);

            wb.SetAllOverridesTo(true);

            wb.temperature.value = wbTemperatere.Value;
            wb.tint.value = wbTint.Value;
        }
        #endregion
    }
}